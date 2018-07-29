using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.ModelsConfig
{
    public class PartsConfig : IEntityTypeConfiguration<Parts>
    {
        public void Configure(EntityTypeBuilder<Parts> builder)
        {
            builder.HasMany(x => x.Cars)
                .WithOne(x => x.Parts)
                .HasForeignKey(x => x.Part_Id);
        }
    }
}