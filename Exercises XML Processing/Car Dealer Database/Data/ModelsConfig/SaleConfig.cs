using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.ModelsConfig
{
    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasOne(x => x.Car)
                .WithMany(s => s.Sales)
                .HasForeignKey(s => s.Car_Id);

            builder.HasOne(c => c.Customer)
                .WithMany(s => s.Sales)
                .HasForeignKey(c => c.Customer_Id);
        }
    }
}