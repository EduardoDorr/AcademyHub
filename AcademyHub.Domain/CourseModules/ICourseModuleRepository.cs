using AcademyHub.Common.Persistence.Repositories;

namespace AcademyHub.Domain.CourseModules;

public interface ICourseModuleRepository
    : IReadableRepository<CourseModule>,
      ICreatableRepository<CourseModule>,
      IUpdatableRepository<CourseModule>
{
    Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default);
}