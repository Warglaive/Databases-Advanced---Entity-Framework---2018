using P01_BillsPaymentSystem.Data.Models.Enums;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public Type Type { get; set; }
        public int UserId { get; set; }
        public int BankAccountId { get; set; }
        public int CreditCardId { get; set; }
    }
}