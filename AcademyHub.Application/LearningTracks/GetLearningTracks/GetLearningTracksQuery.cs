using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Application.LearningTracks.Models;

namespace AcademyHub.Application.LearningTracks.GetLearningTracks;

public sealed record GetLearningTracksQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<LearningTrackViewModel>>>;