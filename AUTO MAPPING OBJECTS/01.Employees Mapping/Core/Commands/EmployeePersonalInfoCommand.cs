using Banicharnica.App.Core.Contracts;

namespace Banicharnica.App.Core.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly IEmployeeController controller;

        public EmployeePersonalInfoCommand(IEmployeeController controller)
        {
            this.controller = controller;
        }
        public string Execute(string[] args)
        {
            var id = int.Parse(args[0]);
            var employee = this.controller.GetEmployeePersonalInfo(id);
            return $"ID: {employee.Id} - {employee.FirstName} {employee.LastName} - ${employee.Salary:f2}" +
            $"Birthday: {employee.Birthday}" +
            $"Address: {employee.Address}";
        }
    }
}