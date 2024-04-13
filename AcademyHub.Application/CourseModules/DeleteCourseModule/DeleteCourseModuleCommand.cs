using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.CourseModules.DeleteCourseModule;

public sealed record DeleteCourseModuleCommand(Guid Id) : IRequest<Result>;