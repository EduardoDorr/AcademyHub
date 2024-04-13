using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Lessons.DeleteLesson;

public sealed record DeleteLessonCommand(Guid Id) : IRequest<Result>;