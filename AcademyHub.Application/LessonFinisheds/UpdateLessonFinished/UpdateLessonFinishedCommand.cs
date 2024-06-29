using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Domain.LessonFinisheds;

namespace AcademyHub.Application.LessonFinisheds.UpdateLessonFinished;

public sealed record UpdateLessonFinishedCommand(
    Guid Id,
    LessonFinishedRating Rating,
    string? Comment) : IRequest<Result>;