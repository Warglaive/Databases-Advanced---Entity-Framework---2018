using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfiguration
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.CourseId);
            builder.Property(x => x.Name).HasMaxLength(80).IsUnicode();
            builder.Property(x => x.Description).IsUnicode().IsRequired(false);

            builder.HasMany(s => s.StudentsEnrolled)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);

            builder.HasMany(r => r.Resources)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);

            builder.HasMany(x => x.HomeworkSubmissions)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);
        }
    }
}