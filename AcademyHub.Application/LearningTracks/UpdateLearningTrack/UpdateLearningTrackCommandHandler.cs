using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.LearningTracks;

namespace AcademyHub.Application.LearningTracks.UpdateLearningTrack;

public sealed class UpdateLearningTrackCommandHandler : IRequestHandler<UpdateLearningTrackCommand, Result>
{
    private readonly ILearningTrackRepository _learningTrackRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLearningTrackCommandHandler(ILearningTrackRepository learningTrackRepository, IUnitOfWork unitOfWork)
    {
        _learningTrackRepository = learningTrackRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateLearningTrackCommand request, CancellationToken cancellationToken)
    {
        var learningTrack = await _learningTrackRepository.GetByIdAsync(request.Id, cancellationToken);

        if (learningTrack is null)
            return Result.Fail(LearningTrackErrors.NotFound);

        learningTrack.Update(
            request.Name,
            request.Description);

        _learningTrackRepository.Update(learningTrack);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(LearningTrackErrors.CannotBeUpdated);

        return Result.Ok();
    }
}