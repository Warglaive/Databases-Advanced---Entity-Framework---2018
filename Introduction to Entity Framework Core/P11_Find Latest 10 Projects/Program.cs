using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P11_Find_Latest_10_Projects
{
    public class Program
    {
        public static void Main()
        {
            var context = new SoftUniContext();
            var projects = context.Projects.OrderByDescending(p => p.StartDate.Year)
                .ThenByDescending(p => p.StartDate.Month)
                .ThenByDescending(p => p.StartDate.Day).Take(10)
                .OrderBy(x => x.Name).ThenByDescending(x => x.Name.Length)
                .Select(n => new
                {
                    n.Name,
                    n.Description,
                    n.StartDate
                });

            foreach (var project in projects)
            {
                Console.WriteLine($"{project.Name}");
                Console.WriteLine($"{project.Description}");
                Console.WriteLine($"{project.StartDate:M/d/yyyy h:mm:ss tt}");
            }
        }
    }
}