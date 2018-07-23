using Banicharnica.App.Core.Contracts;
using Banicharnica.App.Core.DTOs;

namespace Banicharnica.App.Core.Commands
{
    public class AddEmployeeCommand : ICommand
    {
        private readonly IEmployeeController controller;
        public AddEmployeeCommand(IEmployeeController controller)
        {
            this.controller = controller;
        }

        public string Execute(string[] args)
        {
            var firstName = args[0];
            var lastName = args[1];
            var salary = decimal.Parse(args[2]);

            var employeeDto = new EmployeeDto();
            employeeDto.FirstName = firstName;
            employeeDto.LastName = lastName;
            employeeDto.Salary = salary;
            this.controller.AddEmployee(employeeDto);
            return $"Employee: {firstName} {lastName} with {salary} added successfully!";
        }
    }
}