using AcademyHub.Common.DomainEvents;
using AcademyHub.Common.Persistence.UnitOfWork;

using AcademyHub.Domain.Users;
using AcademyHub.Domain.Enrollments;
using AcademyHub.Domain.EnrollmentPayments;

namespace AcademyHub.Application.EnrollmentPayments.EnrollmentPaymentStatusUpdated;

public sealed class EnrollmentPaymentStatusUpdatedEventHandler : IDomainEventHandler<EnrollmentPaymentStatusUpdatedEvent>
{
    private readonly IEnrollmentPaymentRepository _enrollmentPaymentRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollmentPaymentStatusUpdatedEventHandler(
        IEnrollmentPaymentRepository enrollmentPaymentRepository,
        IEnrollmentRepository enrollmentRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _enrollmentPaymentRepository = enrollmentPaymentRepository;
        _enrollmentRepository = enrollmentRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(EnrollmentPaymentStatusUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var enrollmentPayment = await _enrollmentPaymentRepository.GetByExternalId(notification.PaymentId, cancellationToken);

        if (enrollmentPayment is null)
        {
            // do something
            return;
        }

        var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentPayment.EnrollmentId, cancellationToken);

        if (enrollment is null)
        {
            // do something
            return;
        }

        var user = await _userRepository.GetByExternalId(notification.CustomerId, cancellationToken);

        if (user is null)
        {
            // do something
            return;
        }

        if (user.Id != enrollment.UserId)
        {
            // do something
            return;
        }

        enrollmentPayment.SetStatus(notification.PaymentStatus);

        if (notification.PaymentStatus == EnrollmentPaymentStatus.Success)
            enrollment.SetActivedStatus();

        _enrollmentRepository.Update(enrollment);
        _enrollmentPaymentRepository.Update(enrollmentPayment);

        var updated = await _unitOfWork.SaveChangesAsync() > 0;

        if (!updated)
        {
            // do something
            return;
        }
    }
}