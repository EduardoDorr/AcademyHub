using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Application.Courses.Models;

namespace AcademyHub.Application.Courses.GetCourseById;

public sealed record GetCourseByIdQuery(Guid Id) : IRequest<Result<CourseDetailsViewModel?>>;