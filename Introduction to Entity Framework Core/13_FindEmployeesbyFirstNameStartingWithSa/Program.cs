using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P13_FindEmployeesbyFirstNameStartingWithSa
{
    public class Program
    {
        public static void Main()
        {
            var context = new SoftUniContext();
            var employees = context.Employees
                .Where(p => p.FirstName[0] == 'S' && p.FirstName[1] == 'a')
                .Select(p => new Employee
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    JobTitle = p.JobTitle,
                    Salary = p.Salary
                })
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName);

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:f2})");
            }
        }
    }
}