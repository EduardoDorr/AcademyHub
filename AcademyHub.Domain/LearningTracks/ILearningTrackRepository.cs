using AcademyHub.Common.Persistence.Repositories;

namespace AcademyHub.Domain.LearningTracks;

public interface ILearningTrackRepository
    : IReadableRepository<LearningTrack>,
      ICreatableRepository<LearningTrack>,
      IUpdatableRepository<LearningTrack>
{
    Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default);
}