using AcademyHub.Common.Entities;

namespace AcademyHub.Common.Persistence.Repositories;

public interface IDeletableRepository<TEntity> where TEntity : BaseEntity
{
    void Delete(TEntity entity);
}