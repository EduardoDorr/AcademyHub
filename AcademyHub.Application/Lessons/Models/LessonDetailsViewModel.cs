namespace AcademyHub.Application.Lessons.Models;

public sealed record LessonDetailsViewModel(
    Guid Id,
    string Name,
    string Description,
    string VideoLink,
    int Duration,
    decimal AverageRating);