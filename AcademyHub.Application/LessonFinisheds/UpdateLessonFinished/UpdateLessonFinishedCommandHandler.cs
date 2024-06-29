using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;

using AcademyHub.Domain.LessonFinisheds;

namespace AcademyHub.Application.LessonFinisheds.UpdateLessonFinished;

public sealed class UpdateLessonFinishedCommandHandler : IRequestHandler<UpdateLessonFinishedCommand, Result>
{
    private readonly ILessonFinishedRepository _lessonFinishedRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLessonFinishedCommandHandler(ILessonFinishedRepository lessonFinishedRepository, IUnitOfWork unitOfWork)
    {
        _lessonFinishedRepository = lessonFinishedRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateLessonFinishedCommand request, CancellationToken cancellationToken)
    {
        var lessonFinished = await _lessonFinishedRepository.GetByIdAsync(request.Id, cancellationToken);

        if (lessonFinished is null)
            return Result.Fail(LessonFinishedErrors.NotFound);

        lessonFinished.Update(
            request.Rating,
            request.Comment);
        ;

        _lessonFinishedRepository.Update(lessonFinished);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(LessonFinishedErrors.CannotBeUpdated);

        return Result.Ok();
    }
}