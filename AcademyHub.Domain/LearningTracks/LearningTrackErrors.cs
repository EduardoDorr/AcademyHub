using AcademyHub.Common.Results.Errors;

namespace AcademyHub.Domain.LearningTracks;

public sealed record LearningTrackErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("LearningTrack.CannotBeCreated", "Something went wrong and the Learning Track cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("LearningTrack.CannotBeUpdated", "Something went wrong and the Learning Track cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("LearningTrack.CannotBeDeleted", "Something went wrong and the Learning Track cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("LearningTrack.NotFound", "Could not find an active Learning Track", ErrorType.NotFound);

    public static readonly Error IsNotUnique =
        new("LearningTrack.IsNotUnique", "The Learning Track's name is already taken", ErrorType.Conflict);
}