using AcademyHub.Common.DomainEvents;

namespace AcademyHub.Domain.Enrollments;

public sealed record EnrollmentCreatedEvent(
    Guid EnrollmentId,
    DateTime DueDate,
    decimal Value) : IDomainEvent;