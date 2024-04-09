using Microsoft.EntityFrameworkCore;

using AcademyHub.Common.Models.Pagination;

using AcademyHub.Domain.Courses;

using AcademyHub.Infrastructure.Persistence.Contexts;

namespace AcademyHub.Infrastructure.Persistence.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AcademyHubDbContext _dbContext;

    public CourseRepository(AcademyHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<Course>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var users = _dbContext.Courses.AsQueryable();

        return await users.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Courses.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        var hasUser = await _dbContext.Courses.AnyAsync(c => c.Name == name);

        return !hasUser;
    }

    public void Create(Course course)
    {
        _dbContext.Courses.Add(course);
    }

    public void Update(Course course)
    {
        _dbContext.Courses.Update(course);
    }
}