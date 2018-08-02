using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.ModelsConfig
{
    public class PartCarConfig : IEntityTypeConfiguration<PartCar>
    {
        public void Configure(EntityTypeBuilder<PartCar> builder)
        {
            builder.HasKey(x => new
            {
                x.Car_Id,
                x.Part_Id
            });

            builder.HasOne(pc => pc.Part)
                .WithMany(p => p.Cars)
                .HasForeignKey(c => c.Part_Id);

            builder.HasOne(pc => pc.Car)
                .WithMany(p => p.Parts)
                .HasForeignKey(c => c.Car_Id);
        }
    }
}