using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AcademyHub.Common.Persistence.Configurations;

using AcademyHub.Domain.Lessons;

namespace AcademyHub.Infrastructure.Persistence.Configurations;

internal class LessonConfiguration : BaseEntityConfiguration<Lesson>
{
    public override void Configure(EntityTypeBuilder<Lesson> builder)
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

        builder.Property(b => b.VideoLink)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.Duration)
            .IsRequired();

        builder.Property(b => b.AverageRating)
            .HasColumnType("numeric(5,2)")
            .IsRequired();

        builder.Property(b => b.NumberOfRatings)
            .IsRequired();

        builder.HasMany(b => b.CourseModules)
            .WithMany(b => b.Lessons);

        builder.HasMany(b => b.LessonFinisheds)
            .WithOne(b => b.Lesson)
            .HasForeignKey(b => b.LessonId);
    }
}