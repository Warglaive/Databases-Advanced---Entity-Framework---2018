using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;

namespace P07_Employees_and_Projects
{
    public class Program
    {
        public static void Main()
        {
            var context = new SoftUniContext();

            var employees = context.Employees.Where(e =>
                e.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Select(emp => new
                {
                    employeeName = emp.FirstName + " " + emp.LastName,
                    mangerName = emp.Manager.FirstName + " " + emp.Manager.LastName,
                    Projects = emp.EmployeesProjects.Select(s => new
                    {
                        projectName = s.Project.Name,
                        startDate = s.Project.StartDate,
                        endDate = s.Project.EndDate
                    })
                })
                .Take(30)
                .ToArray();

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.employeeName} – Manager: {employee.mangerName}");
                foreach (var project in employee.Projects)
                {
                    if (project.endDate != null)
                    {
                        Console.WriteLine(
                            $"--{project.projectName} - {project.startDate.ToString("M/d/yyyy h:mm:ss tt")} - {project.endDate}");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"--{project.projectName} - {project.startDate.ToString("M/d/yyyy h:mm:ss tt")} - not finished");
                    }
                }
            }
        }
    }
}