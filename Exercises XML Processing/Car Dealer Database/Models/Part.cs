using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Supplier")]
        public int Supplier_Id { get; set; }

        public Supplier Supplier { get; set; }

        public ICollection<PartCar> Cars { get; set; }
    }
}