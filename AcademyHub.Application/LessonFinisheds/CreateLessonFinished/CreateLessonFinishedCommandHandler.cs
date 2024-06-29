using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;

using AcademyHub.Domain.Users;
using AcademyHub.Domain.Lessons;
using AcademyHub.Domain.LessonFinisheds;

namespace AcademyHub.Application.LessonFinisheds.CreateLessonFinished;

public sealed class CreateLessonFinishedCommandHandler : IRequestHandler<CreateLessonFinishedCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly ILessonFinishedRepository _lessonFinishedRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLessonFinishedCommandHandler(
        IUserRepository userRepository,
        ILessonRepository lessonRepository,
        ILessonFinishedRepository lessonFinishedRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _lessonRepository = lessonRepository;
        _lessonFinishedRepository = lessonFinishedRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateLessonFinishedCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return Result.Fail<Guid>(UserErrors.NotFound);

        var lesson = await _lessonRepository.GetByIdAsync(request.LessonId, cancellationToken);

        if (lesson is null)
            return Result.Fail<Guid>(LessonFinishedErrors.NotFound);

        var isUnique = await _lessonFinishedRepository.IsUniqueAsync(request.UserId, request.LessonId, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(LessonFinishedErrors.IsNotUnique);

        var lessonFinishedResult = LessonFinished.Create(
            request.UserId,
            request.LessonId,
            request.Rating,
            request.Comment);

        if (!lessonFinishedResult.Success)
            return Result.Fail<Guid>(lessonFinishedResult.Errors);

        _lessonFinishedRepository.Create(lessonFinishedResult.Value);

        lesson.UpdateRating(request.Rating);

        _lessonRepository.Update(lesson);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(LessonFinishedErrors.CannotBeCreated);

        return Result.Ok(lessonFinishedResult.Value.Id);
    }
}