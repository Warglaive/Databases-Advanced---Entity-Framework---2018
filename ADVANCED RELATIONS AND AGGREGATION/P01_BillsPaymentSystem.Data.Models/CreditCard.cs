using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class CreditCard
    {
        [Key]
        [Required]
        public int CreditCardId { get; set; }
        [Required]
        public decimal Limit { get; set; }
        [Required]
        public decimal MoneyOwed { get; set; }

        [Required]
        [NotMapped]
        public decimal LimitLeft => this.Limit - this.MoneyOwed;
        [Required]
        public DateTime ExpirationDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public decimal Withdraw(decimal value)
        {
            decimal withdrawValue = this.CreditCardId - value;
            if (withdrawValue >= 0)
            {
                Console.WriteLine($"Withdraw Successful");
                this.Limit = withdrawValue;
                return withdrawValue;
            }
            throw new ArgumentException("Insufficient Funds");
        }
        public decimal Deposit(decimal value)
        {
            if (value > 0)
            {
                this.Limit += value;
                Console.WriteLine($"Successfully deposited: {value + Environment.NewLine} Current Balance: {this.Limit}");
                return this.Limit;
            }
            Console.WriteLine($"Unable to deposit funds, try again later");
            return this.Limit;
        }
    }
}