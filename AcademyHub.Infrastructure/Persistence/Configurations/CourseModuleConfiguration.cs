using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AcademyHub.Common.Persistence.Configurations;

using AcademyHub.Domain.CourseModules;

namespace AcademyHub.Infrastructure.Persistence.Configurations;

internal class CourseModuleConfiguration : BaseEntityConfiguration<CourseModule>
{
    public override void Configure(EntityTypeBuilder<CourseModule> builder)
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

        builder.HasMany(b => b.Courses)
            .WithMany(b => b.CourseModules);

        builder.HasMany(b => b.Lessons)
            .WithMany(b => b.CourseModules);
    }
}