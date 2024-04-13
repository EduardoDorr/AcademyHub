using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Application.LearningTracks.Models;

namespace AcademyHub.Application.LearningTracks.GetLearningTrackById;

public sealed record GetLearningTrackByIdQuery(Guid Id) : IRequest<Result<LearningTrackDetailsViewModel?>>;