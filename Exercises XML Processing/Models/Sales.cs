using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Sales
    {
        [Key]
        public int Id { get; set; }

        public int? Discount { get; set; }

        [Required]
        public int Car_Id { get; set; }

        public Cars Cars { get; set; }

        [Required]
        public int Customer_Id { get; set; }

        public Customers Customers { get; set; }
    }
}