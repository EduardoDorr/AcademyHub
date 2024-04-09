using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AcademyHub.Common.Persistence.Configurations;

using AcademyHub.Domain.LearningTracks;

namespace AcademyHub.Infrastructure.Persistence.Configurations;

internal class LearningTrackConfiguration : BaseEntityConfiguration<LearningTrack>
{
    public override void Configure(EntityTypeBuilder<LearningTrack> builder)
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

        builder.HasMany(b => b.Subscriptions)
            .WithMany(b => b.LearningTracks);

        builder.HasMany(b => b.Courses)
            .WithMany(b => b.LearningTracks);
    }
}