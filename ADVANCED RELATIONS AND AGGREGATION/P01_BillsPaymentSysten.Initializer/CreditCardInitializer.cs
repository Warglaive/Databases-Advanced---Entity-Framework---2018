using System;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSysten.Initializer
{
    public class CreditCardInitializer
    {
        public static CreditCard[] GetCreditCards()
        {
            var creditCards = new CreditCard[20];
            for (int i = 0; i < creditCards.Length; i++)
            {
                var creditCard = new CreditCard
                {
                    Limit = i + 1,
                    MoneyOwed = i,
                    ExpirationDate = DateTime.Now.AddMonths(i)
                };
                creditCards[i] = creditCard;
            }
            return creditCards;
        }
    }
}