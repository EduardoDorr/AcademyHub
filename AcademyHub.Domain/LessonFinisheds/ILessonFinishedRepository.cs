using AcademyHub.Common.Persistence.Repositories;

namespace AcademyHub.Domain.LessonFinisheds;

public interface ILessonFinishedRepository
    : IReadableRepository<LessonFinished>,
      ICreatableRepository<LessonFinished>,
      IUpdatableRepository<LessonFinished>
{
    Task<bool> IsUniqueAsync(Guid userId, Guid lessonId, CancellationToken cancellationToken = default);
}