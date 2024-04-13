using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Subscriptions;

namespace AcademyHub.Application.Subscriptions.DeleteSubscription;

public sealed class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, Result>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(request.Id, cancellationToken);

        if (subscription is null)
            return Result.Fail(SubscriptionErrors.NotFound);

        subscription.Deactivate();

        _subscriptionRepository.Update(subscription);

        var deleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!deleted)
            return Result.Fail(SubscriptionErrors.CannotBeDeleted);

        return Result.Ok();
    }
}
