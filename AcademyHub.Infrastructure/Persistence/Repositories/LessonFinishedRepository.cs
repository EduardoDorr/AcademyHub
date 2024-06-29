using Microsoft.EntityFrameworkCore;

using AcademyHub.Common.Models.Pagination;

using AcademyHub.Domain.LessonFinisheds;

using AcademyHub.Infrastructure.Persistence.Contexts;

namespace AcademyHub.Infrastructure.Persistence.Repositories;

public class LessonFinishedRepository : ILessonFinishedRepository
{
    private readonly AcademyHubDbContext _dbContext;

    public LessonFinishedRepository(AcademyHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<LessonFinished>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var users = _dbContext.LessonFinisheds.AsQueryable();

        return await users.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<LessonFinished?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.LessonFinisheds.SingleOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<bool> IsUniqueAsync(Guid userId, Guid lessonId, CancellationToken cancellationToken = default)
    {
        var hasUser = await _dbContext.LessonFinisheds.AnyAsync(l => l.UserId == userId && l.LessonId == lessonId, cancellationToken);

        return !hasUser;
    }

    public void Create(LessonFinished lessonFinished)
    {
        _dbContext.LessonFinisheds.Add(lessonFinished);
    }

    public void Update(LessonFinished lessonFinished)
    {
        _dbContext.LessonFinisheds.Update(lessonFinished);
    }
}