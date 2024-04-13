using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Courses.UpdateCourse;

public sealed record UpdateCourseCommand(
    Guid Id,
    string Name,
    string Description) : IRequest<Result>;