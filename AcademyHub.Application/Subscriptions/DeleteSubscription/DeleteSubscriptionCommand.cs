using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Subscriptions.DeleteSubscription;

public sealed record DeleteSubscriptionCommand(Guid Id) : IRequest<Result>;