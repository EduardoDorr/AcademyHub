using AcademyHub.Domain.LessonFinisheds;

namespace AcademyHub.Application.LessonFinisheds.Models;

public sealed record LessonFinishedDetailsViewModel(
    Guid Id,
    Guid UserId,
    Guid LessonId,
    LessonFinishedRating Rating,
    string? Comment,
    DateTime FinishDate);