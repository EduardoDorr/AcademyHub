using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Application.Subscriptions.Models;

namespace AcademyHub.Application.Subscriptions.GetSubscriptionById;

public sealed record GetSubscriptionByIdQuery(Guid Id) : IRequest<Result<SubscriptionDetailsViewModel?>>;