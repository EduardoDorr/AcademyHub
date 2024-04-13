namespace AcademyHub.Application.Courses.Models;

public sealed record CourseViewModel(
    string Name,
    string Description,
    string? Cover);