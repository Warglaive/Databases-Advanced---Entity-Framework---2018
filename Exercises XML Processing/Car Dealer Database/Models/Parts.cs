using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Parts
    {
        public Parts()
        {
            this.Cars = new List<PartCars>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [Required]
        public int Supplier_id { get; set; }
        public Suppliers Suppliers { get; set; }

        public ICollection<PartCars> Cars { get; set; }
    }
}