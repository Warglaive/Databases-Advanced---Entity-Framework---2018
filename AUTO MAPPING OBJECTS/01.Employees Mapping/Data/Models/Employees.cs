using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeesDb.Data.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public decimal Salary { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
    }
}