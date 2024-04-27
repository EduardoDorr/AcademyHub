using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AcademyHub.Domain.EnrollmentPayments;
using AcademyHub.Common.Persistence.Configurations;

namespace AcademyHub.Infrastructure.Persistence.Configurations;

internal class EnrollmentPaymentConfiguration : BaseEntityConfiguration<EnrollmentPayment>
{
    public override void Configure(EntityTypeBuilder<EnrollmentPayment> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.EnrollmentId)
            .IsRequired();

        builder.Property(b => b.Message)
            .IsRequired();

        builder.Property(b => b.Value)
            .HasColumnType("numeric(8,2)")
            .IsRequired();

        builder.Property(b => b.Status)
            .IsRequired();

        builder.Property(b => b.DueDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(b => b.PaymentLink)
           .IsRequired();

        builder.Property(b => b.PaymentId)
           .IsRequired();

        builder.Property(b => b.ProcessedAt)
            .HasColumnType("datetime");

        builder.HasOne(b => b.Enrollment)
           .WithOne(b => b.EnrollmentPayment)
           .HasForeignKey<EnrollmentPayment>(b => b.EnrollmentId);
    }
}