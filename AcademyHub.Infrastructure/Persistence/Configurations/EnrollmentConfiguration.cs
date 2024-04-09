using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AcademyHub.Common.Persistence.Configurations;

using AcademyHub.Domain.Enrollments;

namespace AcademyHub.Infrastructure.Persistence.Configurations;

internal class EnrollmentConfiguration : BaseEntityConfiguration<Enrollment>
{
    public override void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.UserId)
            .IsRequired();

        builder.Property(b => b.SubscriptionId)
            .IsRequired();

        builder.Property(b => b.Status)
            .IsRequired();

        builder.Property(b => b.StartDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(b => b.ExpirationDate)
            .HasColumnType("datetime")
            .IsRequired();
    }
}