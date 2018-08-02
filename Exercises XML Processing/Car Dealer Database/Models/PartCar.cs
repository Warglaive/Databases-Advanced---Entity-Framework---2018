using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class PartCar
    {
        [Required]
        public int Part_Id { get; set; }
        public Part Part { get; set; }

        [Required]
        public int Car_Id { get; set; }
        public Car Car { get; set; }
    }
}