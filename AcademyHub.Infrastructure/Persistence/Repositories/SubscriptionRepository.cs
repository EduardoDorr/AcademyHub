using Microsoft.EntityFrameworkCore;

using AcademyHub.Common.Models.Pagination;

using AcademyHub.Domain.Subscriptions;

using AcademyHub.Infrastructure.Persistence.Contexts;

namespace AcademyHub.Infrastructure.Persistence.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly AcademyHubDbContext _dbContext;

    public SubscriptionRepository(AcademyHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<Subscription>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var users = _dbContext.Subscriptions.AsQueryable();

        return await users.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Subscriptions
            .Include(c => c.LearningTracks)
            .ThenInclude(c => c.Courses)
            .ThenInclude(c => c.CourseModules)
            .ThenInclude(c => c.Lessons)
            .SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        var hasUser = await _dbContext.Subscriptions.AnyAsync(s => s.Name == name);

        return !hasUser;
    }

    public void Create(Subscription subscription)
    {
        _dbContext.Subscriptions.Add(subscription);
    }

    public void Update(Subscription subscription)
    {
        _dbContext.Subscriptions.Update(subscription);
    }
}