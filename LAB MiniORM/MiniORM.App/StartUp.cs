﻿using System.Linq;
using MiniORM.App.Data;
using MiniORM.App.Data.Entities;

namespace MiniORM.App
{
    public class StartUp
    {
        public static void Main()
        {
            var connectionString = @"Server=WARGLAIVE\SQLEXPRESS;Database=MiniORM;Integrated Security=True";
            var context = new SoftUniDbContextClass(connectionString);
            context.Employees.Add(new Employee
            {
                FirstName = "Gosho",
                LastName = "Inserted",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true
            });
            var employee = context.Employees.Last();
            employee.FirstName = "Modified";
            context.SaveChanges();
        }
    }
}