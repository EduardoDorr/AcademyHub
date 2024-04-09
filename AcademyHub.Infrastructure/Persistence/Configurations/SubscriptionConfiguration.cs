using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AcademyHub.Common.Persistence.Configurations;

using AcademyHub.Domain.Subscriptions;

namespace AcademyHub.Infrastructure.Persistence.Configurations;

internal class SubscriptionConfiguration : BaseEntityConfiguration<Subscription>
{
    public override void Configure(EntityTypeBuilder<Subscription> builder)
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

        builder.Property(b => b.Duration)
            .IsRequired();

        builder.HasMany(b => b.LearningTracks)
            .WithMany(b => b.Subscriptions);

        builder.HasMany(b => b.Enrollments)
            .WithOne(b => b.Subscription)
            .HasForeignKey(b => b.SubscriptionId);
    }
}