using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductShop.Models
{
    public class Products
    {
        public Products()
        {
            this.CategoryProducts = new List<CategoryProducts>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Users Buyer { get; set; }
        public int? BuyerId { get; set; }

        public Users Seller { get; set; }
        [Required]
        public int SellerId { get; set; }

        public ICollection<CategoryProducts> CategoryProducts { get; set; }
    }
}