using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.ModelsConfig
{
    public class PartConfig : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            //builder.HasMany(x => x.Cars)
            //    .WithOne(p => p.Part)
            //    .HasForeignKey(p => p.Part_Id);
        }
    }
}