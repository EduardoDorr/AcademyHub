using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Lessons.UpdateLesson;

public sealed record UpdateLessonCommand(
    Guid Id,
    string Name,
    string Description,
    string VideoLink,
    int Duration) : IRequest<Result>;