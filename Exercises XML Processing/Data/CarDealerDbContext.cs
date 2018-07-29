using Microsoft.EntityFrameworkCore;
namespace Data
{
    public class CarDealerDbContext : DbContext
    {
        public CarDealerDbContext(DbContextOptions options) : base(options)
        {
        }

        public CarDealerDbContext()
        {

        }
    }
}