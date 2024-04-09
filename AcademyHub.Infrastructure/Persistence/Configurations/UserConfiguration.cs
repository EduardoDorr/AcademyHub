using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AcademyHub.Common.Persistence.Configurations;

using AcademyHub.Domain.Users;

namespace AcademyHub.Infrastructure.Persistence.Configurations;

internal class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(b => b.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.BirthDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.OwnsOne(d => d.Cpf,
            cpf =>
            {
                cpf.Property(d => d.Number)
                   .HasColumnName("Cpf")
                   .HasMaxLength(11)
                   .IsRequired();

                cpf.HasIndex(d => d.Number)
                   .IsUnique();
            });

        builder.OwnsOne(d => d.Email,
            email =>
            {
                email.Property(d => d.Address)
                     .HasColumnName("Email")
                     .HasMaxLength(100)
                     .IsRequired();

                email.HasIndex(d => d.Address)
                     .IsUnique();
            });

        builder.OwnsOne(d => d.Telephone,
            telephone =>
            {
                telephone.Property(d => d.Number)
                     .HasColumnName("Telephone")
                     .HasMaxLength(11)
                     .IsRequired();
            });

        builder.OwnsOne(d => d.Password,
            password =>
            {
                password.Property(d => d.Content)
                     .HasColumnName("Password")
                     .HasMaxLength(100)
                     .IsRequired();
            });

        builder.Property(b => b.Role)
            .HasMaxLength(25)
            .IsRequired();

        builder.HasMany(b => b.Enrollments)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);

        builder.HasMany(b => b.LessonFinisheds)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);
    }
}