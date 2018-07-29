using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class PartCars
    {
        [Required]
        public int Part_Id { get; set; }
        public Parts Parts { get; set; }

        [Required]
        public int Car_Id { get; set; }
        public Cars Cars { get; set; }
    }
}