using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Application.Lessons.Models;

namespace AcademyHub.Application.Lessons.GetLessons;

public sealed record GetLessonsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<LessonViewModel>>>;