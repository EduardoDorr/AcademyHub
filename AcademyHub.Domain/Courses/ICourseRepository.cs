using AcademyHub.Common.Persistence.Repositories;

namespace AcademyHub.Domain.Courses;

public interface ICourseRepository
    : IReadableRepository<Course>,
      ICreatableRepository<Course>,
      IUpdatableRepository<Course>
{
    Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default);
}