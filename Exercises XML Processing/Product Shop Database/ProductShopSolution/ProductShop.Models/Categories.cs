using System.ComponentModel.DataAnnotations;

namespace ProductShop.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}