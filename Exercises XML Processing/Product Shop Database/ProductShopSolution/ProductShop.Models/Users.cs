using System.ComponentModel.DataAnnotations;

namespace ProductShop.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? Age { get; set; }
    }
}