using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Application.CourseModules.Models;

namespace AcademyHub.Application.CourseModules.GetCourseModuleById;

public sealed record GetCourseModuleByIdQuery(Guid Id) : IRequest<Result<CourseModuleDetailsViewModel?>>;