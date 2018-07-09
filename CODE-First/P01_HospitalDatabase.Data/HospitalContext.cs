using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.EntityConfiguration;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientMedicament> PatientMedicaments { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DiagnoseConfig());
            modelBuilder.ApplyConfiguration(new DoctorsConfig());
            modelBuilder.ApplyConfiguration(new MedicamentConfig());
            modelBuilder.ApplyConfiguration(new PatientConfig());
            modelBuilder.ApplyConfiguration(new PatientMedicamentConfig());
            modelBuilder.ApplyConfiguration(new VisitationConfig());
        }
    }
}
