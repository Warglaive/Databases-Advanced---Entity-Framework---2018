using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Cars
    {
        public Cars()
        {
            this.Parts = new List<PartCars>();
        }
        [Key]
        public int Id { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public double TravelledDistance { get; set; }

        public ICollection<PartCars> Parts { get; set; }
    }
}