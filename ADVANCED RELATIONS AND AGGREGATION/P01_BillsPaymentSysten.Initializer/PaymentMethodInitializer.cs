using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSysten.Initializer
{
    public class PaymentMethodInitializer
    {
        public static PaymentMethod[] GetPaymentMethods()
        {
            var paymentMethods = new PaymentMethod[20];
            for (int i = 0; i < paymentMethods.Length; i++)
            {
                var current = new PaymentMethod()
                {
                    User = UserInitializer.GetUsers()[i],
                    CreditCard = CreditCardInitializer.GetCreditCards()[i],
                    BankAccount = BankAccountInitializer.GetBankAccounts()[i]
                };

                paymentMethods[i] = current;
            }
            return paymentMethods;
        }
    }
}