using AcademyHub.Common.Entities;

namespace AcademyHub.Common.Persistence.Repositories;

public interface IGenericRepository<TEntity>
    : IReadableRepository<TEntity>,
      ICreatableRepository<TEntity>,
      IUpdatableRepository<TEntity>,
      IDeletableRepository<TEntity> where TEntity : BaseEntity
{
}