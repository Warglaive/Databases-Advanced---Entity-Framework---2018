using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.ModelsConfig
{
    public class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            //builder.HasMany(p => p.Parts)
            //    .WithOne(x => x.Car)
            //    .HasForeignKey(x => x.Car_Id);
        }
    }
}