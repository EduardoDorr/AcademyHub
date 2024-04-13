using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Application.Courses.Models;

namespace AcademyHub.Application.Courses.GetCourses;

public sealed record GetCoursesQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<CourseViewModel>>>;