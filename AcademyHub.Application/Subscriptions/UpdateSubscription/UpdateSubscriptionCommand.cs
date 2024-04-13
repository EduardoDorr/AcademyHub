using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Subscriptions.UpdateSubscription;

public sealed record UpdateSubscriptionCommand(
    Guid Id,
    string Name,
    string Description,
    int Duration) : IRequest<Result>;