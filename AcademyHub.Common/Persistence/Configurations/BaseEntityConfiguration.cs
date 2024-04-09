using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AcademyHub.Common.Entities;

namespace AcademyHub.Common.Persistence.Configurations;

public abstract class BaseEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Active)
               .IsRequired();

        builder.HasIndex(b => b.Active);

        builder.HasQueryFilter(b => b.Active);

        builder.Property(b => b.CreatedAt)
               .HasColumnType("datetime")
               .IsRequired();

        builder.Property(b => b.UpdatedAt)
               .HasColumnType("datetime")
               .IsRequired();
    }
}