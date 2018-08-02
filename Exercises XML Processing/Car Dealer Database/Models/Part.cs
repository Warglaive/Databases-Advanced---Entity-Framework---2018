using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Part
    {
        public Part()
        {
            this.Cars = new List<PartCar>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [Required]
        public int Supplier_id { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<PartCar> Cars { get; set; }
    }
}