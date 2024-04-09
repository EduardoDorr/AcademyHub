using AcademyHub.Common.Results.Errors;

namespace AcademyHub.Domain.CourseModules;

public sealed record CourseModuleErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("CourseModule.CannotBeCreated", "Something went wrong and the Course Module cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("CourseModule.CannotBeUpdated", "Something went wrong and the Course Module cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("CourseModule.CannotBeDeleted", "Something went wrong and the Course Module cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("CourseModule.NotFound", "Could not find an active Course Module", ErrorType.NotFound);

    public static readonly Error IsNotUnique =
        new("CourseModule.IsNotUnique", "The Course Module's name is already taken", ErrorType.Conflict);
}