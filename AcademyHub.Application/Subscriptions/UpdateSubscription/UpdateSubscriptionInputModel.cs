namespace AcademyHub.Application.Subscriptions.UpdateSubscription;

public sealed record UpdateSubscriptionInputModel(
    string Name,
    string Description,
    int Duration);