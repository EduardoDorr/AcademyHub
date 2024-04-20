using AcademyHub.Common.Results;

namespace AcademyHub.Domain.CourseModules;

public interface ICourseModuleService
{
    Task<Result> AddLessonsAsync(CourseModule courseModule, IList<Guid> lessonsId, CancellationToken cancellationToken = default);
    Task<Result> RemoveLessons(CourseModule courseModule, IList<Guid> lessonsId, CancellationToken cancellationToken = default);
}