using Microsoft.EntityFrameworkCore;

using AcademyHub.Common.Models.Pagination;

using AcademyHub.Domain.Lessons;

using AcademyHub.Infrastructure.Persistence.Contexts;

namespace AcademyHub.Infrastructure.Persistence.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly AcademyHubDbContext _dbContext;

    public LessonRepository(AcademyHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<Lesson>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var users = _dbContext.Lessons.AsQueryable();

        return await users.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<Lesson?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Lessons.SingleOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        var hasUser = await _dbContext.Lessons.AnyAsync(l => l.Name == name);

        return !hasUser;
    }

    public void Create(Lesson lesson)
    {
        _dbContext.Lessons.Add(lesson);
    }

    public void Update(Lesson lesson)
    {
        _dbContext.Lessons.Update(lesson);
    }
}