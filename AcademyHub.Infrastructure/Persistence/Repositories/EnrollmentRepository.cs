using Microsoft.EntityFrameworkCore;

using AcademyHub.Common.Models.Pagination;

using AcademyHub.Domain.Enrollments;

using AcademyHub.Infrastructure.Persistence.Contexts;

namespace AcademyHub.Infrastructure.Persistence.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AcademyHubDbContext _dbContext;

    public EnrollmentRepository(AcademyHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<Enrollment>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var users = _dbContext.Enrollments.AsQueryable();

        return await users.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<Enrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Enrollments
            .Include(c => c.User)
            .Include(c => c.Subscription)
            .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<bool> IsUniqueAsync(Guid userId, Guid subscriptionId, CancellationToken cancellationToken = default)
    {
        var hasUser = await _dbContext.Enrollments.AnyAsync(e => e.UserId == userId && e.SubscriptionId == subscriptionId);

        return !hasUser;
    }

    public void Create(Enrollment enrollment)
    {
        _dbContext.Enrollments.Add(enrollment);
    }

    public void Update(Enrollment enrollment)
    {
        _dbContext.Enrollments.Update(enrollment);
    }
}