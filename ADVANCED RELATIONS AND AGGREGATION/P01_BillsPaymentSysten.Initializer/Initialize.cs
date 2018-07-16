using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using P01_BillsPaymentSystem.Data;

namespace P01_BillsPaymentSysten.Initializer
{
    public class Initialize
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            InsertUsers(context);
            InsertBankAccounts(context);
            InsertCreditCards(context);
            InsertPaymentMethods(context);
        }
        public static void InsertPaymentMethods(BillsPaymentSystemContext context)
        {
            var paymentMethods = PaymentMethodInitializer.GetPaymentMethods();
            for (var i = 0; i < paymentMethods.Length; i++)
            {
                if (IsValid(paymentMethods[i]))
                {
                    context.PaymentMethods.Add(paymentMethods[i]);
                }
            }
            context.SaveChanges();
        }
        public static void InsertBankAccounts(BillsPaymentSystemContext context)
        {
            var bankAccounts = BankAccountInitializer.GetBankAccounts();
            for (var i = 0; i < bankAccounts.Length; i++)
            {
                if (IsValid(bankAccounts[i]))
                {
                    context.BankAccounts.Add(bankAccounts[i]);
                }
            }
            context.SaveChanges();
        }
        public static void InsertUsers(BillsPaymentSystemContext context)
        {
            var users = UserInitializer.GetUsers();
            for (var i = 0; i < users.Length; i++)
            {
                if (IsValid(users[i]))
                {
                    context.Users.Add(users[i]);
                }
            }
            context.SaveChanges();
        }
        public static void InsertCreditCards(BillsPaymentSystemContext context)
        {
            var creditCards = CreditCardInitializer.GetCreditCards();
            for (var i = 0; i < creditCards.Length; i++)
            {
                if (IsValid(creditCards[i]))
                {
                    context.CreditCards.Add(creditCards[i]);
                }
            }
            context.SaveChanges();
        }
        public static bool IsValid(Object obj)
        {
            var validationContext = new ValidationContext(obj);
            var result = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, validationContext, result, true);
        }
    }
}