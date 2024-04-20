using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Subscriptions.AddLearningTrack;

public sealed record AddLearningTrackCommand(
    Guid SubscriptionId,
    IList<Guid> LearningTracksId) : IRequest<Result>;