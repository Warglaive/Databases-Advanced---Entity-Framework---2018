using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.ModelsConfig
{
    public class SalesConfig : IEntityTypeConfiguration<Sales>
    {
        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            builder.HasOne(x => x.Cars);
            builder.HasOne(x => x.Customers)
                .WithMany(c => c.Cars.Count)
        }
    }
}