using AcademyHub.Common.DomainEvents;

namespace AcademyHub.Domain.EnrollmentPayments;

public sealed record EnrollmentPaymentStatusUpdatedEvent(
    string PaymentId,
    string CustomerId,
    EnrollmentPaymentStatus PaymentStatus) : IDomainEvent;