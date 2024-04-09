using Microsoft.EntityFrameworkCore;

using AcademyHub.Common.Models.Pagination;

using AcademyHub.Domain.LearningTracks;

using AcademyHub.Infrastructure.Persistence.Contexts;

namespace AcademyHub.Infrastructure.Persistence.Repositories;

public class LearningTrackRepository : ILearningTrackRepository
{
    private readonly AcademyHubDbContext _dbContext;

    public LearningTrackRepository(AcademyHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<LearningTrack>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var users = _dbContext.LearningTracks.AsQueryable();

        return await users.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<LearningTrack?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.LearningTracks.SingleOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        var hasUser = await _dbContext.LearningTracks.AnyAsync(l => l.Name == name);

        return !hasUser;
    }

    public void Create(LearningTrack learningTrack)
    {
        _dbContext.LearningTracks.Add(learningTrack);
    }

    public void Update(LearningTrack learningTrack)
    {
        _dbContext.LearningTracks.Update(learningTrack);
    }
}