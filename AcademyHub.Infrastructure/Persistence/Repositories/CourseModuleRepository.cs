using Microsoft.EntityFrameworkCore;

using AcademyHub.Common.Models.Pagination;

using AcademyHub.Domain.CourseModules;

using AcademyHub.Infrastructure.Persistence.Contexts;

namespace AcademyHub.Infrastructure.Persistence.Repositories;

public class CourseModuleRepository : ICourseModuleRepository
{
    private readonly AcademyHubDbContext _dbContext;

    public CourseModuleRepository(AcademyHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<CourseModule>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var users = _dbContext.CourseModules.AsQueryable();

        return await users.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<CourseModule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CourseModules.Include(c => c.Lessons).SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        var hasUser = await _dbContext.CourseModules.AnyAsync(c => c.Name == name);

        return !hasUser;
    }

    public void Create(CourseModule courseModule)
    {
        _dbContext.CourseModules.Add(courseModule);
    }

    public void Update(CourseModule courseModule)
    {
        _dbContext.CourseModules.Update(courseModule);
    }
}