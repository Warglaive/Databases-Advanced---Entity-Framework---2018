using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Suppliers
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsImporter { get; set; }
    }
}