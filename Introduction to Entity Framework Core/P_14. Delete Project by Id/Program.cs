using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P_14._Delete_Project_by_Id
{
    public class Program
    {
        public static void Main()
        {
            var context = new SoftUniContext();
            var employeesProjects = context.EmployeesProjects.Where(i => i.Project.ProjectId == 2);
            var projects = context.Projects.Where(id => id.ProjectId == 2);
            context.RemoveRange(employeesProjects);
            context.RemoveRange(projects);
            context.SaveChanges();
            var result = context.Projects.Select(x => x.Name).Take(10);
            foreach (var project in result)
            {
                Console.WriteLine(project);
            }
        }
    }
}