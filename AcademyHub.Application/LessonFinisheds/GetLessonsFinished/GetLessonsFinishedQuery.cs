using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Application.LessonFinisheds.Models;

namespace AcademyHub.Application.LessonFinisheds.GetLessonsFinished;

public sealed record GetLessonsFinishedQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<LessonFinishedViewModel>>>;