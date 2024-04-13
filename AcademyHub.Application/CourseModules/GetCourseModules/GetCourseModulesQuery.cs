using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Application.CourseModules.Models;

namespace AcademyHub.Application.CourseModules.GetCourseModules;

public sealed record GetCourseModulesQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<CourseModuleViewModel>>>;