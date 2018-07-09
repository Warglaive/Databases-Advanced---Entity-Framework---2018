using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class PatientMedicamentConfig : IEntityTypeConfiguration<PatientMedicament>
    {
        public void Configure(EntityTypeBuilder<PatientMedicament> builder)
        {
            builder.HasKey(x => new
            {
                x.MedicamentId,
                x.PatientId
            });

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Prescriptions)
                .HasForeignKey(x => x.PatientId);
        }
    }
}
