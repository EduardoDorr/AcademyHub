using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Services;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.LearningTracks;
using AcademyHub.Domain.Subscriptions;

namespace AcademyHub.Application.Subscriptions.RemoveLearningTrack;

public sealed class RemoveLearningTrackCommandHandler : IRequestHandler<RemoveLearningTrackCommand, Result>
{
    ISubscriptionRepository _subscriptionRepository;
    ILearningTrackRepository _learningTrackModuleRepository;
    IUnitOfWork _unitOfWork;

    public RemoveLearningTrackCommandHandler(ISubscriptionRepository subscriptionRepository, ILearningTrackRepository learningTrackRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _learningTrackModuleRepository = learningTrackRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveLearningTrackCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId, cancellationToken);

        if (subscription is null)
            return Result.Fail(SubscriptionErrors.NotFound);

        var learningTracksResult = await RepositoryExtension
            .GetEntitiesByIdAsync(_learningTrackModuleRepository, request.LearningTracksId, cancellationToken);

        if (!learningTracksResult.Success)
            return Result.Fail(learningTracksResult.Errors);

        subscription.RemoveLearningTracks(learningTracksResult.Value);

        _subscriptionRepository.Update(subscription);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(SubscriptionErrors.CannotBeUpdated);

        return Result.Ok();
    }
}