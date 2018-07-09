using System;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P12_Increase_Salaries
{
    public class Program
    {
        public static void Main()
        {
            var context = new SoftUniContext();
            var employees = context.Employees
                .Where(e => e.Department.Name == "Engineering"
                      || e.Department.Name == "Tool Design"
                      || e.Department.Name == "Marketing"
                      || e.Department.Name == "Information Services")
                      
                      .OrderBy(x => x.FirstName)
                      .ThenBy(x => x.LastName);


            foreach (var employee in employees)
            {
                employee.Salary *= 1.12m;
                Console.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");

            }
        }
    }
}