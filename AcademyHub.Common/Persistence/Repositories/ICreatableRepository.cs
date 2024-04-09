using AcademyHub.Common.Entities;

namespace AcademyHub.Common.Persistence.Repositories;

public interface ICreatableRepository<TEntity> where TEntity : BaseEntity
{
    void Create(TEntity entity);
}