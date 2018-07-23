using System;
using System.Linq;
using System.Text;
using Banicharnica.App.Core.Contracts;

namespace Banicharnica.App.Core.Commands
{
    public class ListEmployeesOlderThan : ICommand
    {
        private readonly IEmployeeController controller;
        public ListEmployeesOlderThan(IEmployeeController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            var age = int.Parse(args[0]);
            var employees = this.controller.GetEmployeesOlderThan(age).OrderByDescending(x => x.Salary);
            var sb = new StringBuilder();


            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - ${e.Salary:f2} - Manager: []");
            }

            return sb.ToString().Trim();
        }
    }
}
