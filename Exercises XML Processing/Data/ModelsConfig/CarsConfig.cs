using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.ModelsConfig
{
    public class CarsConfig : IEntityTypeConfiguration<Cars>
    {
        public void Configure(EntityTypeBuilder<Cars> builder)
        {
            builder.HasMany(p => p.Parts)
                .WithOne(c => c.Cars)
                .HasForeignKey(x => x.Car_Id);

        }
    }
}