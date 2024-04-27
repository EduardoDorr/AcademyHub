namespace AcademyHub.Application.Users.Enroll;

public sealed record EnrollInputModel(
    Guid SubscriptionId,
    decimal Value);