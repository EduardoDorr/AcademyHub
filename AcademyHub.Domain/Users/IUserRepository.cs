﻿using AcademyHub.Common.Persistence.Repositories;

namespace AcademyHub.Domain.Users;

public interface IUserRepository
    : IReadableRepository<User>,
      ICreatableRepository<User>,
      IUpdatableRepository<User>
{
    Task<bool> IsUniqueAsync(string cpf, string email, CancellationToken cancellationToken = default);
}