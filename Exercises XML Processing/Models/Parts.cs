using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Parts
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [Required]
        public int Supplier_id { get; set; }
    }
}