using System.Collections.Generic;

namespace Banicharnica.App.Core.DTOs
{
    public class ManagerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<EmployeeDto> EmployeesDto { get; set; }
    }
}