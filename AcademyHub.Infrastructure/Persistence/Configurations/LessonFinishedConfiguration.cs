using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AcademyHub.Common.Persistence.Configurations;

using AcademyHub.Domain.LessonFinisheds;

namespace AcademyHub.Infrastructure.Persistence.Configurations;

internal class LessonFinishedConfiguration : BaseEntityConfiguration<LessonFinished>
{
    public override void Configure(EntityTypeBuilder<LessonFinished> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.UserId)
            .IsRequired();

        builder.Property(b => b.LessonId)
            .IsRequired();

        builder.Property(b => b.Comment)
            .HasMaxLength(500);

        builder.Property(b => b.Rating)
            .IsRequired();

        builder.Property(b => b.FinishDate)
            .HasColumnType("datetime")
            .IsRequired();
    }
}