using System;
using System.Linq;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSysten.Initializer;

namespace P01_BillsPaymentSystem
{
    public class StartUp
    {
        public static void Main()
        {
            var input = int.Parse(Console.ReadLine());
            using (var context = new BillsPaymentSystemContext())
            {
                context.Database.EnsureCreated();
                Initialize.Seed(context);
                Initialize.UserDetails(input, context);
                context.Database.EnsureDeleted();
            }
            var amount = decimal.Parse(Console.ReadLine());
            PayBills(input, amount);
        }
        public static void PayBills(int userId, decimal amount)
        {
            using (var context = new BillsPaymentSystemContext())
            {
                var user = context.Users.FirstOrDefault(x => x.UserId == userId);

                foreach (var userPaymentMethod in user.PaymentMethods)
                {
                    userPaymentMethod.BankAccount.Balance -= amount;
                }
            }
        }
    }
}