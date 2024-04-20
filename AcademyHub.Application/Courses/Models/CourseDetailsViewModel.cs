using AcademyHub.Application.CourseModules.Models;

namespace AcademyHub.Application.Courses.Models;

public sealed record CourseDetailsViewModel(
    Guid Id,
    string Name,
    string Description,
    string? Cover,
    int Duration,
    IReadOnlyCollection<CourseModuleDetailsViewModel> CourseModules);