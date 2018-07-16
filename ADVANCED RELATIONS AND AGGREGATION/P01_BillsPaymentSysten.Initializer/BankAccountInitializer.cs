using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSysten.Initializer
{
    public class BankAccountInitializer
    {
        public static BankAccount[] GetBankAccounts()
        {
            var bankAccounts = new BankAccount[20];
            for (int i = 0; i < bankAccounts.Length; i++)
            {
                var currentAcc = new BankAccount
                {
                    Balance = i + 100,
                    BankName = $"N.{i}Bank",
                    SwiftCode = $"{i}.{i + 1}.{i + 2}.{i + 3}"
                };
                bankAccounts[i] = currentAcc;
            }
            return bankAccounts;
        }
    }
}