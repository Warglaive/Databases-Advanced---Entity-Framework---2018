using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductShop.Models;

namespace ProductShop.Data.ModelsConfig
{
    public class CategoriesConfig : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.HasMany(x => x.CategoryProducts);
        }
    }
}