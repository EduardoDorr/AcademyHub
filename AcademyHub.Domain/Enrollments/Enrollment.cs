using AcademyHub.Common.Results;
using AcademyHub.Common.Entities;

using AcademyHub.Domain.Users;
using AcademyHub.Domain.Subscriptions;
using AcademyHub.Domain.EnrollmentPayments;

namespace AcademyHub.Domain.Enrollments;

public sealed class Enrollment : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid SubscriptionId { get; private set; }
    public EnrollmentStatus Status { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime ExpirationDate { get; private set; }

    public User User { get; private set; }
    public Subscription Subscription { get; private set; }
    public EnrollmentPayment EnrollmentPayment { get; private set; }

    protected Enrollment() { }

    private Enrollment(
        Guid userId,
        Guid subscriptionId,
        DateTime startDate,
        DateTime expirationDate)
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
        Status = EnrollmentStatus.Pending;
        StartDate = startDate;
        ExpirationDate = expirationDate;
    }

    public static Result<Enrollment> Create(
        Guid userId,
        Guid subscriptionId,
        DateTime startDate,
        DateTime expirationDate,
        decimal value)
    {
        if (!ValidateStartAndExpirationDates(startDate, expirationDate))
            return Result.Fail<Enrollment>(EnrollmentErrors.ExpirationDateIsInvalid);

        var enrollment =
            new Enrollment(
                userId,
                subscriptionId,
                startDate,
                expirationDate);

        var createdEvent =
            new EnrollmentCreatedEvent(
                enrollment.Id,
                startDate.AddDays(7),
                value);

        enrollment.RaiseDomainEvent(createdEvent);

        return Result<Enrollment>.Ok(enrollment);
    }

    public Result Update(DateTime startDate, DateTime expirationDate)
    {
        if (!ValidateStartAndExpirationDates(startDate, expirationDate))
            return Result.Fail(EnrollmentErrors.ExpirationDateIsInvalid);

        StartDate = startDate;
        ExpirationDate = expirationDate;

        return Result.Ok();
    }

    public void SetActivedStatus() =>
        Status = EnrollmentStatus.Actived;

    public void SetDeactivedStatus() =>
        Status = EnrollmentStatus.Deactived;

    public void SetExpiredStatus() =>
        Status = EnrollmentStatus.Expired;

    private static bool ValidateStartAndExpirationDates(DateTime startDate, DateTime expirationDate)
    {
        if (startDate.CompareTo(expirationDate) > 0)
            return false;

        return true;
    }
}