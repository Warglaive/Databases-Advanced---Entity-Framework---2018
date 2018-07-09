using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P06._AddingaNewAddressandUpdatingEmployee
{
    public class Program
    {
        public static void Main()
        {
            var context = new SoftUniContext();

            var newAddress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };
            var employee = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");
            employee.Address = newAddress;
            context.SaveChanges();

            var result = context.Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10).Select(a => a.Address.AddressText);

            foreach (var e in result)
            {
                Console.WriteLine(e);
            }
        }
    }
}