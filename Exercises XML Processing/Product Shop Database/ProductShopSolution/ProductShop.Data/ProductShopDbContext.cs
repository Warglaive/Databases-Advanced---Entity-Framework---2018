using Microsoft.EntityFrameworkCore;
using ProductShop.Data.ModelsConfig;
using ProductShop.Models;

namespace ProductShop.Data
{
    public class ProductShopDbContext : DbContext
    {
        public DbSet<Categories> Categories { get; set; }
        public DbSet<CategoryProducts> CategoryProducts { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Users> Users { get; set; }

        public ProductShopDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        public ProductShopDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=WARGLAIVE\SQLEXPRESS;Database=ProductShopDb;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriesConfig());
            modelBuilder.ApplyConfiguration(new CategoryProductsConfig());
            modelBuilder.ApplyConfiguration(new ProductsConfig());
            modelBuilder.ApplyConfiguration(new UsersConfig());
        }
    }
}
