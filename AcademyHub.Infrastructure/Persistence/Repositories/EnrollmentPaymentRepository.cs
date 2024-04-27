using Microsoft.EntityFrameworkCore;

using AcademyHub.Common.Models.Pagination;

using AcademyHub.Domain.EnrollmentPayments;
using AcademyHub.Infrastructure.Persistence.Contexts;

namespace AcademyHub.Infrastructure.Persistence.Repositories;

public class EnrollmentPaymentRepository : IEnrollmentPaymentRepository
{
    private readonly AcademyHubDbContext _dbContext;

    public EnrollmentPaymentRepository(AcademyHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<EnrollmentPayment>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var users = _dbContext.EnrollmentPayments.AsQueryable();

        return await users.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<EnrollmentPayment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.EnrollmentPayments
            .Include(c => c.Enrollment)
            .ThenInclude(c => c.Subscription)
            .Include(c => c.Enrollment)
            .ThenInclude(c => c.User)
            .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Create(EnrollmentPayment enrollmentPayment)
    {
        _dbContext.EnrollmentPayments.Add(enrollmentPayment);
    }
}