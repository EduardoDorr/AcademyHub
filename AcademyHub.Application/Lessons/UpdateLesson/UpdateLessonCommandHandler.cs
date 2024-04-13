using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Lessons;

namespace AcademyHub.Application.Lessons.UpdateLesson;

public sealed class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, Result>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    {
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.Id, cancellationToken);

        if (lesson is null)
            return Result.Fail(LessonErrors.NotFound);

        lesson.Update(
            request.Name,
            request.Description,
            request.VideoLink,
            request.Duration);
        ;

        _lessonRepository.Update(lesson);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(LessonErrors.CannotBeUpdated);

        return Result.Ok();
    }
}