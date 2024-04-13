using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Lessons.CreateLesson;

public sealed record CreateLessonCommand(
    string Name,
    string Description,
    string VideoLink,
    int Duration) : IRequest<Result<Guid>>;