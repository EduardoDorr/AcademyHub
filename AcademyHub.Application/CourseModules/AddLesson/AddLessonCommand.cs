using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.CourseModules.AddLesson;

public sealed record AddLessonCommand(
    Guid CourseModuleId,
    IList<Guid> LessonsId) : IRequest<Result>;