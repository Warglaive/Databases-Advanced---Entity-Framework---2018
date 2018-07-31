using Microsoft.EntityFrameworkCore;

namespace ProductShop.Data
{
    public class ProductShopDbContext : DbContext
    {
        public ProductShopDbContext(DbContextOptions options) : base(options)
        {
        }

        public ProductShopDbContext()
        {
        }
    }
}
