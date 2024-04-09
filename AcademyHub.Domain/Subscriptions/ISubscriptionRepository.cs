using AcademyHub.Common.Persistence.Repositories;

namespace AcademyHub.Domain.Subscriptions;

public interface ISubscriptionRepository
    : IReadableRepository<Subscription>,
      ICreatableRepository<Subscription>,
      IUpdatableRepository<Subscription>
{
    Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default);
}