using AcademyHub.Domain.LessonFinisheds;

namespace AcademyHub.Application.LessonFinisheds.UpdateLessonFinished;

public sealed record UpdateLessonFinishedInputModel(
    LessonFinishedRating Rating,
    string? Comment);