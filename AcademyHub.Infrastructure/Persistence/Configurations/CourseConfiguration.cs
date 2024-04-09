using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AcademyHub.Common.Persistence.Configurations;

using AcademyHub.Domain.Courses;

namespace AcademyHub.Infrastructure.Persistence.Configurations;

internal class CourseConfiguration : BaseEntityConfiguration<Course>
{
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(b => b.Name)
            .IsUnique();

        builder.Property(b => b.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(b => b.Cover)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasMany(b => b.LearningTracks)
            .WithMany(b => b.Courses);

        builder.HasMany(b => b.CourseModules)
            .WithMany(b => b.Courses);
    }
}