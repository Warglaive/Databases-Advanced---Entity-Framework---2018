﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Car
    {
        public Car()
        {
            this.Parts = new List<PartCar>();
            this.Sales = new List<Sale>();
        }
        [Key]
        public int Id { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public double TravelledKm { get; set; }

        public ICollection<PartCar> Parts { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}