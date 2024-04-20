using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.CourseModules.RemoveLesson;

public sealed record RemoveLessonCommand(
    Guid CourseModuleId,
    IList<Guid> LessonsId) : IRequest<Result>;