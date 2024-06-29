using AcademyHub.Domain.LessonFinisheds;

namespace AcademyHub.Application.LessonFinisheds.Models;

public sealed record LessonFinishedViewModel(
    Guid Id,
    Guid UserId,
    Guid LessonId,
    LessonFinishedRating Rating);