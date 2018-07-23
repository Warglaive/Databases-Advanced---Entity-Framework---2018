using AutoMapper;
using P01_EmployeesMapping.DTOs;

namespace P01_EmployeesMapping
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new EmployeesDbContext();
            Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeDto>());
            var employee = context.Employees.FirstOrDefault();
            var employeeDto = Mapper.Map<EmployeeDto>(employee);
        }
    }
}