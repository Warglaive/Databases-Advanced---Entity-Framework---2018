using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductShop.Models;

namespace ProductShop.Data.ModelsConfig
{
    public class CategoryProductsConfig : IEntityTypeConfiguration<CategoryProducts>
    {
        public void Configure(EntityTypeBuilder<CategoryProducts> builder)
        {
            builder.HasKey(x => new {x.CategoryId, x.ProductId});
        }
    }
}