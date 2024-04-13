using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.CourseModules.CreateCourseModule;

public sealed record CreateCourseModuleCommand(
    string Name,
    string Description) : IRequest<Result<Guid>>;