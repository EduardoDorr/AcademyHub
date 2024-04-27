using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Users;
using AcademyHub.Domain.Enrollments;
using AcademyHub.Domain.Subscriptions;
using AcademyHub.Application.Abstractions.Models;
using AcademyHub.Application.Abstractions.PaymentGateway;

namespace AcademyHub.Application.Users.Enroll;

public sealed class EnrollCommandHandler : IRequestHandler<EnrollCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentGateway _paymentGateway;

    public EnrollCommandHandler(
        IUserRepository userRepository,
        ISubscriptionRepository subscriptionRepository,
        IEnrollmentRepository enrollmentRepository,
        IUnitOfWork unitOfWork,
        IPaymentGateway paymentGateway)
    {
        _userRepository = userRepository;
        _subscriptionRepository = subscriptionRepository;
        _enrollmentRepository = enrollmentRepository;
        _unitOfWork = unitOfWork;
        _paymentGateway = paymentGateway;
    }

    public async Task<Result<Guid>> Handle(EnrollCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return Result.Fail<Guid>(UserErrors.NotFound);

        var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId, cancellationToken);

        if (subscription is null)
            return Result.Fail<Guid>(SubscriptionErrors.NotFound);

        var enrollmentResult =
            Enrollment.Create(
                user.Id,
                subscription.Id,
                DateTime.Today,
                DateTime.Today.AddDays(subscription.Duration),
                request.Value);

        if (!enrollmentResult.Success)
            return Result.Fail<Guid>(enrollmentResult.Errors);

        var enrollment = enrollmentResult.Value;

        _enrollmentRepository.Create(enrollment);

        var customerModel =
            new CustomerModel(
                user.Id,
                $"{user.FirstName} {user.LastName}",
                user.Cpf.Number,
                user.Email.Address,
                user.Telephone.Number);

        var paymentGatewayClientIdResult = await _paymentGateway.CreateClientAsync(customerModel);

        if (!paymentGatewayClientIdResult.Success)
            return Result.Fail<Guid>(paymentGatewayClientIdResult.Errors);

        user.SetPaymentGatewayClientId(paymentGatewayClientIdResult.Value);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(EnrollmentErrors.CannotBeCreated);

        return Result.Ok(enrollment.Id);
    }
}