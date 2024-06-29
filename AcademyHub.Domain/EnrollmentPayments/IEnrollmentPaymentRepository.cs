using AcademyHub.Common.Persistence.Repositories;

namespace AcademyHub.Domain.EnrollmentPayments;

public interface IEnrollmentPaymentRepository
    : IReadableRepository<EnrollmentPayment>,
      ICreatableRepository<EnrollmentPayment>,
      IUpdatableRepository<EnrollmentPayment>
{
    Task<EnrollmentPayment?> GetByExternalId(string externalId, CancellationToken cancellationToken = default);
}