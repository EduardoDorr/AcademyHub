using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Courses.RemoveModule;

public sealed record RemoveCourseModuleCommand(
    Guid CourseId,
    IList<Guid> CourseModulesId) : IRequest<Result>;