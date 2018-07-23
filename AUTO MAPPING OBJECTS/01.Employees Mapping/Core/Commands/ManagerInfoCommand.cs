using System;
using System.Text;
using Banicharnica.App.Core.Contracts;

namespace Banicharnica.App.Core.Commands
{
    public class ManagerInfoCommand : ICommand
    {
        private readonly IManagerController controller;
        public ManagerInfoCommand(IManagerController controller)
        {
            this.controller = controller;
        }

        public string Execute(string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var managerDto = this.controller.GetManagerInfo(employeeId);
            var sb = new StringBuilder();
            sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.EmployeesDto.Count}");
            foreach (var e in managerDto.EmployeesDto)
            {
                sb.AppendLine($"- {e.FirstName} {e.LastName} - ${e.Salary:f2}");
            }
            return sb.ToString().Trim();
        }
    }
}
