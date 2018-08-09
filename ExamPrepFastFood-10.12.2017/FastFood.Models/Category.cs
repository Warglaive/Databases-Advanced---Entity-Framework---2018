using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Category
    {
        public Category()
        {
            this.Items = new List<Item>();
        }
        [Key]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3), Required]
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}