using AcademyHub.Common.Results.Errors;

namespace AcademyHub.Domain.Lessons;

public sealed record LessonErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("Lesson.CannotBeCreated", "Something went wrong and the Lesson cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("Lesson.CannotBeUpdated", "Something went wrong and the Lesson cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("Lesson.CannotBeDeleted", "Something went wrong and the Lesson cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("Lesson.NotFound", "Could not find an active Lesson", ErrorType.NotFound);

    public static readonly Error IsNotUnique =
        new("Lesson.IsNotUnique", "The Lesson's name is already taken", ErrorType.Conflict);
}