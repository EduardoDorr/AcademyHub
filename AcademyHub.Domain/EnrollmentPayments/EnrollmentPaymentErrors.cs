using AcademyHub.Common.Results.Errors;

namespace AcademyHub.Domain.EnrollmentPayments;

public sealed record EnrollmentPaymentErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("EnrollmentPayment.CannotBeCreated", "Something went wrong and the Enrollment cannot be created", ErrorType.Failure);

    public static readonly Error NotFound =
        new("EnrollmentPayment.NotFound", "Could not find an active Enrollment", ErrorType.NotFound);
}