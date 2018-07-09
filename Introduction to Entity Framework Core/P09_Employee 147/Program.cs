using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P09_Employee_147
{
    public class Program
    {
        public static void Main()
        {
            var context = new SoftUniContext();
            var employees = context.Employees
                .Where(a => a.EmployeeId == 147)
                .Select(e => new
                {
                    Name = e.FirstName + " " + e.LastName,
                    e.JobTitle,
                    Projects = e.EmployeesProjects.Select(p => new
                    {
                        ProjectName = p.Project.Name
                    }).OrderBy(x => x.ProjectName)
                });

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Name} - {employee.JobTitle}");
                foreach (var project in employees.Select(e => e.Projects))
                {
                    Console.WriteLine(string.Join(Environment.NewLine, project.Select(a => a.ProjectName)));
                }
            }
        }
    }
}
