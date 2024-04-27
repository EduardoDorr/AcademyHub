using AcademyHub.Common.Persistence.Repositories;

namespace AcademyHub.Domain.EnrollmentPayments;

public interface IEnrollmentPaymentRepository
    : IReadableRepository<EnrollmentPayment>,
      ICreatableRepository<EnrollmentPayment>
{ }