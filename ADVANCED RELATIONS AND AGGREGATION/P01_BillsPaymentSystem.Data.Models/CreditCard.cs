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
    }
}