using Banicharnica.App.Core.Contracts;

namespace Banicharnica.App.Core.Commands
{
    public class EmployeeInfoCommand : ICommand
    {
        private readonly IEmployeeController controller;

        public EmployeeInfoCommand(IEmployeeController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            var id = int.Parse(args[0]);
            var employeeDto = this.controller.GetEmployeeInfo(id);
            return $"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} -  ${employeeDto.Salary:f2}";
        }
    }
}