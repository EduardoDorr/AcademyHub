using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Lessons;

namespace AcademyHub.Application.Lessons.CreateLesson;

public sealed class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, Result<Guid>>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLessonCommandHandler(ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    {
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _lessonRepository.IsUniqueAsync(request.Name, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(LessonErrors.IsNotUnique);

        var lessonResult = Lesson.Create(
            request.Name,
            request.Description,
            request.VideoLink,
            request.Duration);

        if (!lessonResult.Success)
            return Result.Fail<Guid>(lessonResult.Errors);

        _lessonRepository.Create(lessonResult.Value);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(LessonErrors.CannotBeCreated);

        return Result.Ok(lessonResult.Value.Id);
    }
}