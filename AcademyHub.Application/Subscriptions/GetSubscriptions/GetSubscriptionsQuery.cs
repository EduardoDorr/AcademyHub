using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Application.Subscriptions.Models;

namespace AcademyHub.Application.Subscriptions.GetSubscriptions;

public sealed record GetSubscriptionsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<SubscriptionViewModel>>>;