using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Subscriptions;

namespace AcademyHub.Application.Subscriptions.UpdateSubscription;

public sealed class UpdateSubscriptionCommandHandler : IRequestHandler<UpdateSubscriptionCommand, Result>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(request.Id, cancellationToken);

        if (subscription is null)
            return Result.Fail(SubscriptionErrors.NotFound);

        subscription.Update(
            request.Name,
            request.Description,
            request.Duration);

        _subscriptionRepository.Update(subscription);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(SubscriptionErrors.CannotBeUpdated);

        return Result.Ok();
    }
}