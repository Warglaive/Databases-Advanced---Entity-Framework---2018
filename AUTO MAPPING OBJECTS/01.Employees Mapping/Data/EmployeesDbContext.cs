using EmployeesDb.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesDb.Data
{
    public class EmployeesDbContext : DbContext
    {
        public DbSet<Employees> Employees { get; set; }
        public EmployeesDbContext(DbContextOptions options) : base(options)
        {
        }

        public EmployeesDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }
    }
}