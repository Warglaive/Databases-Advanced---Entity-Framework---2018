using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P08_Addresses_by_Town
{
    class Program
    {
        static void Main()
        {
            var context = new SoftUniContext();
            var addresses = context.Addresses
                .Select(t => new
                {
                    addressText = t.AddressText,
                    TownName = t.Town.Name,
                    EmployeeCount = t.Employees.Count
                }).OrderByDescending(t => t.EmployeeCount).ThenBy(t => t.TownName)
                .ThenBy(t => t.addressText)
                .Take(10);

            foreach (var address in addresses)
            {
                Console.WriteLine($"{address.addressText}, {address.TownName} - {address.EmployeeCount} employees");
            }
        }
    }
}
