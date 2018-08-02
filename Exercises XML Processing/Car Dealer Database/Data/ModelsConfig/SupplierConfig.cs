using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.ModelsConfig
{
    class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasMany(p => p.Parts)
                .WithOne(s => s.Supplier)
                .HasForeignKey(s => s.Supplier_Id);
        }
    }
}