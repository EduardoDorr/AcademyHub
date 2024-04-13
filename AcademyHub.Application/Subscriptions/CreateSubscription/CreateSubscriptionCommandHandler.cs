using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Subscriptions;

namespace AcademyHub.Application.Subscriptions.CreateSubscription;

public sealed class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Result<Guid>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _subscriptionRepository.IsUniqueAsync(request.Name, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(SubscriptionErrors.IsNotUnique);

        var subscriptionResult = Subscription.Create(
            request.Name,
            request.Description,
            request.Duration);

        if (!subscriptionResult.Success)
            return Result.Fail<Guid>(subscriptionResult.Errors);

        _subscriptionRepository.Create(subscriptionResult.Value);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(SubscriptionErrors.CannotBeCreated);

        return Result.Ok(subscriptionResult.Value.Id);
    }
}