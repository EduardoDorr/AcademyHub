using AcademyHub.Common.Persistence.Repositories;

namespace AcademyHub.Domain.Users;

public interface IUserRepository
    : IReadableRepository<User>,
      ICreatableRepository<User>,
      IUpdatableRepository<User>
{
    Task<User?> GetUserByEmailAndPasswordAsync(string email, string passwordHash);
    Task<bool> IsUniqueAsync(string cpf, string email, CancellationToken cancellationToken = default);
    Task<User?> GetByExternalId(string externalId, CancellationToken cancellationToken = default);
}