using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public double TravelledDistance { get; set; }
    }
}