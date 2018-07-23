using Banicharnica.Models;
using Microsoft.EntityFrameworkCore;

namespace Banicharnica.Data
{
    public class BanicharnicaContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public BanicharnicaContext(DbContextOptions options) : base(options)
        {
        }

        public BanicharnicaContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}