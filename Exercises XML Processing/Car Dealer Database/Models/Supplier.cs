using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Supplier
    {
        public Supplier()
        {
            this.Parts = new List<Part>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsImported { get; set; }

        public ICollection<Part> Parts { get; set; }
    }
}