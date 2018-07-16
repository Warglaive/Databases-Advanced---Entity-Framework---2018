using System;
using System.ComponentModel.DataAnnotations;
using P01_BillsPaymentSystem.Data.Models.Attributes;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        [MaxLength(50)]
        public string BankName { get; set; }
        [Required]
        [NonUnicode]
        [MaxLength(20)]
        public string SwiftCode { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public decimal Withdraw(decimal value)
        {
            decimal withdrawValue = this.BankAccountId - value;
            if (withdrawValue >= 0)
            {
                Console.WriteLine($"Withdraw Successful");
                this.Balance = withdrawValue;
                return withdrawValue;
            }
            throw new ArgumentException("Insufficient Funds");
        }

        public decimal Deposit(decimal value)
        {
            if (value > 0)
            {
                this.Balance += value;
                Console.WriteLine($"Successfully deposited: {value + Environment.NewLine} Current Balance: {this.Balance}");
                return this.Balance;
            }
            Console.WriteLine($"Unable to deposit funds, try again later");
            return this.Balance;
        }
    }
}