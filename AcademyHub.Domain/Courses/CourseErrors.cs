using AcademyHub.Common.Results.Errors;

namespace AcademyHub.Domain.Courses;

public sealed record CourseErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("Course.CannotBeCreated", "Something went wrong and the Course cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("Course.CannotBeUpdated", "Something went wrong and the Course cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("Course.CannotBeDeleted", "Something went wrong and the Course cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("Course.NotFound", "Could not find an active Course", ErrorType.NotFound);

    public static readonly Error IsNotUnique =
        new("Course.IsNotUnique", "The Course's name is already taken", ErrorType.Conflict);
}