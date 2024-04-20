using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Courses.AddModule;

public sealed record AddCourseModuleCommand(
    Guid CourseId,
    IList<Guid> CourseModulesId) : IRequest<Result>;