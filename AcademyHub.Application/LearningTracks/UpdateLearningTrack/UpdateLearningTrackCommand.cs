using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.LearningTracks.UpdateLearningTrack;

public sealed record UpdateLearningTrackCommand(
    Guid Id,
    string Name,
    string Description) : IRequest<Result>;