using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfiguration
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.StudentId);
            builder.Property(x => x.Name).HasMaxLength(100).IsUnicode();
            builder.Property(x => x.PhoneNumber).HasMaxLength(10)
                .IsUnicode(false).IsRequired(false);
            builder.Property(b => b.Birthday).IsRequired();

            builder.HasMany(x => x.CourseEnrollments)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.CourseId);

            builder.HasMany(x => x.HomeworkSubmissions)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId);
        }
    }
}