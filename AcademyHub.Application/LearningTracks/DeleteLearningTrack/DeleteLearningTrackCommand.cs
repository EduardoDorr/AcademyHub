using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.LearningTracks.DeleteLearningTrack;

public sealed record DeleteLearningTrackCommand(Guid Id) : IRequest<Result>;