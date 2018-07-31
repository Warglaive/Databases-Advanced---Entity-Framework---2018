using System.ComponentModel.DataAnnotations;

namespace ProductShop.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int? BuyerId { get; set; }

        [Required]
        public int SellerId { get; set; }
    }
}