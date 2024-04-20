using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Services;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.LearningTracks;
using AcademyHub.Domain.Subscriptions;

namespace AcademyHub.Application.Subscriptions.AddLearningTrack;

public sealed class AddLearningTrackCommandHandler : IRequestHandler<AddLearningTrackCommand, Result>
{
    ISubscriptionRepository _subscriptionRepository;
    ILearningTrackRepository _learningTrackRepository;
    IUnitOfWork _unitOfWork;

    public AddLearningTrackCommandHandler(ISubscriptionRepository subscriptionRepository, ILearningTrackRepository learningTrackRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _learningTrackRepository = learningTrackRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddLearningTrackCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId, cancellationToken);

        if (subscription is null)
            return Result.Fail(SubscriptionErrors.NotFound);

        var learningTracksResult = await RepositoryExtension
            .GetEntitiesByIdAsync(_learningTrackRepository, request.LearningTracksId, cancellationToken);

        if (!learningTracksResult.Success)
            return Result.Fail(learningTracksResult.Errors);

        subscription.AddLearningTracks(learningTracksResult.Value);

        _subscriptionRepository.Update(subscription);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(SubscriptionErrors.CannotBeUpdated);

        return Result.Ok();
    }
}