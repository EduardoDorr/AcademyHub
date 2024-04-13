using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Domain.Subscriptions;
using AcademyHub.Application.Subscriptions.Models;

namespace AcademyHub.Application.Subscriptions.GetSubscriptions;

public sealed class GetSubscriptionsQueryHandler : IRequestHandler<GetSubscriptionsQuery, Result<PaginationResult<SubscriptionViewModel>>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IMapper _mapper;

    public GetSubscriptionsQueryHandler(ISubscriptionRepository subscriptionRepository, IMapper mapper)
    {
        _subscriptionRepository = subscriptionRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginationResult<SubscriptionViewModel>>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var paginationSubscriptions = await _subscriptionRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var subscriptionsViewModel = _mapper.Map<List<SubscriptionViewModel>>(paginationSubscriptions.Data);

        var paginationSubscriptionsViewModel = paginationSubscriptions.Map(subscriptionsViewModel);

        return Result.Ok(paginationSubscriptionsViewModel);
    }
}