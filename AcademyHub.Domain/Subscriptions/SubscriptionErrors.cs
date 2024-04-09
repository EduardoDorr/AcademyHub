using AcademyHub.Common.Results.Errors;

namespace AcademyHub.Domain.Subscriptions;

public sealed record SubscriptionErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("Subscription.CannotBeCreated", "Something went wrong and the Subscription cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("Subscription.CannotBeUpdated", "Something went wrong and the Subscription cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("Subscription.CannotBeDeleted", "Something went wrong and the Subscription cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("Subscription.NotFound", "Could not find an active Subscription", ErrorType.NotFound);

    public static readonly Error IsNotUnique =
        new("Subscription.IsNotUnique", "The Subscription's name is already taken", ErrorType.Conflict);
}