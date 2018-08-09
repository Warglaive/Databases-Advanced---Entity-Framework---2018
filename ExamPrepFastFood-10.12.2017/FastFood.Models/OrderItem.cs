using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public Order Order { get; set; }

        [Key]
        public int ItemId { get; set; }

        [Required]
        public Item Item { get; set; }

        [Range(1, int.MaxValue), Required]
        public int Quantity { get; set; }
    }
}