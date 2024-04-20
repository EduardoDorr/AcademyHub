using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Subscriptions.RemoveLearningTrack;

public sealed record RemoveLearningTrackCommand(
    Guid SubscriptionId,
    IList<Guid> LearningTracksId) : IRequest<Result>;