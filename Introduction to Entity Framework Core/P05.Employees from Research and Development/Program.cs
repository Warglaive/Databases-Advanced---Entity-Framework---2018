using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;

namespace P05.Employees_from_Research_and_Development
{
    public class Program
    {
        public static void Main()
        {
            var context = new SoftUniContext();
            var employees =
                context.Employees.Include(d => d.Department)
                    .Where(e => e.Department.Name == "Research And Development")
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName);

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:f2}");
            }
        }
    }
}