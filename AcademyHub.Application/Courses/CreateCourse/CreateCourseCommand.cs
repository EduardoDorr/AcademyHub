using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Courses.CreateCourse;

public sealed record CreateCourseCommand(
    string Name,
    string Description,
    string? Cover) : IRequest<Result<Guid>>;