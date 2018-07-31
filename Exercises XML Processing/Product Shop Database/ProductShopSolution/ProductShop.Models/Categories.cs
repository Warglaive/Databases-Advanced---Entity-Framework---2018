using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductShop.Models
{
    public class Categories
    {
        public Categories()
        {
            this.CategoryProducts = new List<CategoryProducts>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }

        public ICollection<CategoryProducts> CategoryProducts { get; set; }
    }
}