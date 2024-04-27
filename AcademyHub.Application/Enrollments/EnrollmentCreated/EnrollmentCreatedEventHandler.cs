using AcademyHub.Common.DomainEvents;
using AcademyHub.Common.Persistence.UnitOfWork;

using AcademyHub.Domain.Users;
using AcademyHub.Domain.Enrollments;
using AcademyHub.Domain.EnrollmentPayments;

using AcademyHub.Application.Abstractions.Models;
using AcademyHub.Application.Abstractions.PaymentGateway;

namespace AcademyHub.Application.Enrollments.EnrollmentCreated;

public sealed class EnrollmentCreatedEventHandler : IDomainEventHandler<EnrollmentCreatedEvent>
{
    private readonly IEnrollmentPaymentRepository _enrollmentPaymentRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentGateway _paymentGateway;

    public EnrollmentCreatedEventHandler(
        IEnrollmentPaymentRepository enrollmentPaymentRepository,
        IEnrollmentRepository enrollmentRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPaymentGateway paymentGateway)
    {
        _enrollmentPaymentRepository = enrollmentPaymentRepository;
        _enrollmentRepository = enrollmentRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _paymentGateway = paymentGateway;
    }

    public async Task Handle(EnrollmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(notification.EnrollmentId, cancellationToken);

        if (enrollment is null)
        {
            // do something
            return;
        }

        var user = await _userRepository.GetByIdAsync(enrollment.UserId, cancellationToken);

        if (user is null)
        {
            // do something
            return;
        }

        var createPaymentModel =
            new CreatePaymentModel(
                user.PaymentGatewayClientId,
                notification.Value,
                notification.DueDate,
                enrollment.Subscription.Name);

        var paymentCreatedResult =
            await _paymentGateway.CreatePaymentAsync(createPaymentModel);

        if (!paymentCreatedResult.Success)
        {
            // do somenthing
            return;
        }

        var paymentCreated = paymentCreatedResult.Value;

        var enrollmentPaymentResult =
            EnrollmentPayment.Create(
                notification.EnrollmentId,
                enrollment.Subscription.Name,
                notification.Value,
                notification.DueDate,
                paymentCreated.InvoiceUrl,
                paymentCreated.PaymentId);

        if (!enrollmentPaymentResult.Success)
        {
            // do somenthing
            return;
        }

        _enrollmentPaymentRepository.Create(enrollmentPaymentResult.Value);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
        {
            // do somenthing
            return;
        }
    }
}