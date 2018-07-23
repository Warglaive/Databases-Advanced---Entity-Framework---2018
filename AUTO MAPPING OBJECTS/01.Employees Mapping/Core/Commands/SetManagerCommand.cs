using System;
using Banicharnica.App.Core.Contracts;

namespace Banicharnica.App.Core.Commands
{
    public class SetManagerCommand : ICommand
    {
        private readonly IManagerController controller;

        public SetManagerCommand(IManagerController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var managerId = int.Parse(args[1]);
            this.controller.SetManagerCommand(employeeId, managerId);
            return $"command successful";
        }
    }
}