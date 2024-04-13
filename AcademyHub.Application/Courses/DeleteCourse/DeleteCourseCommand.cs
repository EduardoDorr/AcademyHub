using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Courses.DeleteCourse;

public sealed record DeleteCourseCommand(Guid Id) : IRequest<Result>;