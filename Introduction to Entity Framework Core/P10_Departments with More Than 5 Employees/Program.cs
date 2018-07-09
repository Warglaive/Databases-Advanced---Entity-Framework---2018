using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P10_Departments_with_More_Than_5_Employees
{
    public class Program
    {
        public static void Main()
        {
            var context = new SoftUniContext();
            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(n => new
                {
                    DepartmentName = n.Name,
                    ManagerName = $"{n.Manager.FirstName} {n.Manager.LastName}",
                    Employees = n.Employees.Select(p => new
                    {
                        p.FirstName,
                        p.LastName,
                        p.JobTitle
                    })
                });

            foreach (var department in departments)
            {
                Console.WriteLine($"{department.DepartmentName} - {department.ManagerName}");
                foreach (var employee in department.Employees.OrderBy(p => p.FirstName).ThenBy(p => p.LastName))
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
                Console.WriteLine("----------");
            }
        }
    }
}