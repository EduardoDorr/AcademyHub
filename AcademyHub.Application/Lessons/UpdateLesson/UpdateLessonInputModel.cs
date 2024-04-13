namespace AcademyHub.Application.Lessons.UpdateLesson;

public sealed record UpdateLessonInputModel(
    string Name,
    string Description,
    string VideoLink,
    int Duration);