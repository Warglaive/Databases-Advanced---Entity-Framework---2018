using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        public int? DiscountPercentage { get; set; }

        [Required]
        public int Car_Id { get; set; }
        public Car Car { get; set; }

        [Required]
        public int Customer_Id { get; set; }
        public Customer Customer { get; set; }
    }
}