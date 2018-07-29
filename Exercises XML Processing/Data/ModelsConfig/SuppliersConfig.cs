using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.ModelsConfig
{
    class SuppliersConfig : IEntityTypeConfiguration<Suppliers>
    {
        public void Configure(EntityTypeBuilder<Suppliers> builder)
        {
            builder.HasMany(x => x.Parts)
                .WithOne(s => s.Suppliers)
                .HasForeignKey(s => s.Supplier_id);
        }
    }
}