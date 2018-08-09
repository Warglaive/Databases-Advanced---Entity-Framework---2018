using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFood.Models
{
    public class Item
    {
        public Item()
        {
            this.OrderItems = new List<OrderItem>();
        }
        [Key]
        public int Id { get; set; }

        [MinLength(3), MaxLength(30), Required]
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; }

        [Range(0.01, double.MaxValue), Required]
        public decimal Price { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}