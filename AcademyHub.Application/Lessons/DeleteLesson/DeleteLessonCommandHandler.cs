using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Lessons;

namespace AcademyHub.Application.Lessons.DeleteLesson;

public sealed class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, Result>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    {
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.Id, cancellationToken);

        if (lesson is null)
            return Result.Fail(LessonErrors.NotFound);

        lesson.Deactivate();

        _lessonRepository.Update(lesson);

        var deleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!deleted)
            return Result.Fail(LessonErrors.CannotBeDeleted);

        return Result.Ok();
    }
}
