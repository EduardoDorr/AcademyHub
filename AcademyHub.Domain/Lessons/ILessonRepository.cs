using AcademyHub.Common.Persistence.Repositories;

namespace AcademyHub.Domain.Lessons;

public interface ILessonRepository
    : IReadableRepository<Lesson>,
      ICreatableRepository<Lesson>,
      IUpdatableRepository<Lesson>
{
    Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default);
}