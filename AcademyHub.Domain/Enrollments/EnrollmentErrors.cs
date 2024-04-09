using AcademyHub.Common.Results.Errors;

namespace AcademyHub.Domain.Enrollments;

public sealed record EnrollmentErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("Enrollment.CannotBeCreated", "Something went wrong and the Enrollment cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("Enrollment.CannotBeUpdated", "Something went wrong and the Enrollment cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("Enrollment.CannotBeDeleted", "Something went wrong and the Enrollment cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("Enrollment.NotFound", "Could not find an active Enrollment", ErrorType.NotFound);

    public static readonly Error ExpirationDateIsInvalid =
        new("Enrollment.ExpirationDateIsInvalid", "The Expiration Date must be later than Start Date", ErrorType.Validation);
}