using AcademyHub.Common.Entities;

namespace AcademyHub.Common.Persistence.Repositories;

public interface IUpdatableRepository<TEntity> where TEntity : BaseEntity
{
    void Update(TEntity entity);
}