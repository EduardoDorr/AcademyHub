using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.LearningTracks.CreateLearningTrack;

public sealed record CreateLearningTrackCommand(
    string Name,
    string Description,
    string? Cover) : IRequest<Result<Guid>>;