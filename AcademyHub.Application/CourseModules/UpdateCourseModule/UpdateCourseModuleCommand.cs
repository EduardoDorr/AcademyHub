using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.CourseModules.UpdateCourseModule;

public sealed record UpdateCourseModuleCommand(
    Guid Id,
    string Name,
    string Description) : IRequest<Result>;