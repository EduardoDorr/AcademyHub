using System.Reflection;

using Microsoft.EntityFrameworkCore;

using AcademyHub.Common.Persistence.Outbox;
using AcademyHub.Common.Persistence.Configurations;

using AcademyHub.Domain.Users;
using AcademyHub.Domain.Courses;
using AcademyHub.Domain.Lessons;
using AcademyHub.Domain.Constants;
using AcademyHub.Domain.Enrollments;
using AcademyHub.Domain.CourseModules;
using AcademyHub.Domain.Subscriptions;
using AcademyHub.Domain.LearningTracks;
using AcademyHub.Domain.LessonFinisheds;
using AcademyHub.Domain.EnrollmentPayments;

namespace AcademyHub.Infrastructure.Persistence.Contexts;

public class AcademyHubDbContext : DbContext
{
    public DbSet<CourseModule> CourseModules { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<EnrollmentPayment> EnrollmentPayments { get; set; }
    public DbSet<LearningTrack> LearningTracks { get; set; }
    public DbSet<LessonFinished> LessonFinisheds { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    //public DbSet<IntegrationEvent> IntegrationEvents { get; set; }

    public AcademyHubDbContext(DbContextOptions<AcademyHubDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaConstants.AcademyHubSchema);
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}