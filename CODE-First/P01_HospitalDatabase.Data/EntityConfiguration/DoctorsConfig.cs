using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class DoctorsConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(x => x.DoctorId);
            builder.Property(x => x.Name).HasMaxLength(100).IsUnicode();
            builder.Property(x => x.Specialty).HasMaxLength(100).IsUnicode();
        }
    }
}
