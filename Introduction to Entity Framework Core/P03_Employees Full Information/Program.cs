using System;
using P02_DatabaseFirst.Data;

namespace P03_Employees_Full_Information
{
    public class Program
    {
        public static void Main()
        {
            var context = new SoftUniContext();
            var employees = context.Employees;
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }
        }
    }
}