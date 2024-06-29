using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Domain.LessonFinisheds;

namespace AcademyHub.Application.LessonFinisheds.CreateLessonFinished;

public sealed record CreateLessonFinishedCommand(
    Guid UserId,
    Guid LessonId,
    LessonFinishedRating Rating,
    string? Comment) : IRequest<Result<Guid>>;