using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductShop.Models
{
    public class Users
    {
        public Users()
        {
            this.ProductsSold = new List<Products>();
            this.ProductsBought = new List<Products>();
        }
        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public ICollection<Products> ProductsSold { get; set; }
        public ICollection<Products> ProductsBought { get; set; }
    }
}