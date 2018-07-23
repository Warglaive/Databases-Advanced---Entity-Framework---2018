using System;
using System.Globalization;
using Banicharnica.App.Core.Contracts;

namespace Banicharnica.App.Core.Commands
{
    public class SetBirthdayCommand : ICommand
    {
        private const string BirthdayDateSetSuccessfully = "Birthday Date Set Successfully!";
        private readonly IEmployeeController controller;
        public SetBirthdayCommand(IEmployeeController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            var id = int.Parse(args[0]);
            DateTime date = DateTime.ParseExact(args[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);
            this.controller.SetBirthday(id, date);
            return BirthdayDateSetSuccessfully;
        }
    }
}