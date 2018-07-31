using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Suppliers
    {
        public Suppliers()
        {
            this.Parts = new List<Parts>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public ICollection<Parts> Parts { get; set; }
    }
}