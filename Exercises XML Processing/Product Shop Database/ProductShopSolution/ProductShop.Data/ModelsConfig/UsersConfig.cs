using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductShop.Models;

namespace ProductShop.Data.ModelsConfig
{
    public class UsersConfig : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasMany(x => x.ProductsBought)
                .WithOne(x => x.Buyer)
                .HasForeignKey(x => x.BuyerId);

            builder.HasMany(x => x.ProductsSold)
                .WithOne(x => x.Seller)
                .HasForeignKey(x => x.SellerId);
        }
    }
}