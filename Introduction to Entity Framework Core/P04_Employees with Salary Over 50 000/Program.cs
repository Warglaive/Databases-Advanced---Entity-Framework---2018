using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P04_Employees_with_Salary_Over_50_000
{
    public class Program
    {
        public static void Main()
        {
            var context = new SoftUniContext();
            var employees = context.Employees.Where(s => s.Salary > 50000)
                .OrderBy(s => s.FirstName);
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.FirstName);
            }
        }
    }
}