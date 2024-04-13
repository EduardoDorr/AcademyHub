using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.LearningTracks;

namespace AcademyHub.Application.LearningTracks.CreateLearningTrack;

public sealed class CreateLearningTrackCommandHandler : IRequestHandler<CreateLearningTrackCommand, Result<Guid>>
{
    private readonly ILearningTrackRepository _learningTrackRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLearningTrackCommandHandler(ILearningTrackRepository learningTrackRepository, IUnitOfWork unitOfWork)
    {
        _learningTrackRepository = learningTrackRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateLearningTrackCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _learningTrackRepository.IsUniqueAsync(request.Name, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(LearningTrackErrors.IsNotUnique);

        var learningTrackResult = LearningTrack.Create(
            request.Name,
            request.Description,
            request.Cover);

        if (!learningTrackResult.Success)
            return Result.Fail<Guid>(learningTrackResult.Errors);

        _learningTrackRepository.Create(learningTrackResult.Value);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(LearningTrackErrors.CannotBeCreated);

        return Result.Ok(learningTrackResult.Value.Id);
    }
}