using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(x => x.BankAccountId);
            builder.Property(x => x.BankName).HasMaxLength(50).IsUnicode();
            builder.Property(x => x.SwiftCode).HasMaxLength(20).IsUnicode(false);
        }
    }
}