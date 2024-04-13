using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Domain.Subscriptions;
using AcademyHub.Application.Subscriptions.Models;

namespace AcademyHub.Application.Subscriptions.GetSubscriptionById;

public sealed class GetSubscriptionByIdQueryHandler : IRequestHandler<GetSubscriptionByIdQuery, Result<SubscriptionDetailsViewModel?>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IMapper _mapper;

    public GetSubscriptionByIdQueryHandler(ISubscriptionRepository subscriptionRepository, IMapper mapper)
    {
        _subscriptionRepository = subscriptionRepository;
        _mapper = mapper;
    }

    public async Task<Result<SubscriptionDetailsViewModel?>> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(request.Id, cancellationToken);

        if (subscription is null)
            return Result.Fail<SubscriptionDetailsViewModel?>(SubscriptionErrors.NotFound);

        var subscriptionViewModel = _mapper.Map<SubscriptionDetailsViewModel?>(subscription);

        return Result.Ok(subscriptionViewModel);
    }
}