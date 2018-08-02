﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Customer
    {
        public Customer()
        {
            this.Sales = new List<Sale>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}