using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Subscriptions.CreateSubscription;

public sealed record CreateSubscriptionCommand(
    string Name,
    string Description,
    int Duration) : IRequest<Result<Guid>>;