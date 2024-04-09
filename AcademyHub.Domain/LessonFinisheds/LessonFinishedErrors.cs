using AcademyHub.Common.Results.Errors;

namespace AcademyHub.Domain.LessonFinisheds;

public sealed record LessonFinishedErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("LessonFinished.CannotBeCreated", "Something went wrong and the Lesson cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("LessonFinished.CannotBeUpdated", "Something went wrong and the Lesson cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("LessonFinished.CannotBeDeleted", "Something went wrong and the Lesson cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("LessonFinished.NotFound", "Could not find an active Lesson", ErrorType.NotFound);

    public static readonly Error IsNotUnique =
        new("LessonFinished.IsNotUnique", "User already finished the lesson", ErrorType.Conflict);
}