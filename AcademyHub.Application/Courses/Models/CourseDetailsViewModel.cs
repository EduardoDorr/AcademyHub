using AcademyHub.Application.CourseModules.Models;

namespace AcademyHub.Application.Courses.Models;

public sealed record CourseDetailsViewModel(
    Guid Id,
    string Name,
    string Description,
    string? Cover,
    IReadOnlyCollection<CourseModuleDetailsViewModel> CourseModules);