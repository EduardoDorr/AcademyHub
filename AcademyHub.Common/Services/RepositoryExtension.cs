using AcademyHub.Common.Entities;
using AcademyHub.Common.Results;
using AcademyHub.Common.Results.Errors;
using AcademyHub.Common.Persistence.Repositories;

namespace AcademyHub.Common.Services;

public static class RepositoryExtension
{
    public static async Task<Result<List<T>>> GetEntitiesByIdAsync<T>(
        IReadableRepository<T> repository,
        IList<Guid>? entitiesId,
        CancellationToken cancellationToken) where T : BaseEntity
    {
        if (entitiesId is null || entitiesId.Count == 0)
            return Result.Ok(new List<T>());

        var entities = new List<T>();

        foreach (var entityId in entitiesId)
        {
            var entity = await repository.GetByIdAsync(entityId, cancellationToken);
            entities.Add(entity);
        }

        if (entities.Any(s => s is null))
        {
            var entitiesNotFound = entitiesId.Except(entities.Where(s => s is not null).Select(s => s.Id));
            var entitiesErrors = entitiesNotFound.Select(id => new Error($"{nameof(T)}NotFound", $"Not found id {id}", ErrorType.NotFound));

            return Result.Fail<List<T>>(entitiesErrors);
        }

        return Result.Ok(entities.ToList());
    }
}