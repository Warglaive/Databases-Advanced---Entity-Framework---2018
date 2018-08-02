using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Customer
    {
        public Customer()
        {
            // this.Cars = new List<Car>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }

        //    public ICollection<Car> Cars { get; set; }
    }
}