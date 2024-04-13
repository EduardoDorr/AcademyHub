using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.LearningTracks;

namespace AcademyHub.Application.LearningTracks.DeleteLearningTrack;

public sealed class DeleteLearningTrackCommandHandler : IRequestHandler<DeleteLearningTrackCommand, Result>
{
    private readonly ILearningTrackRepository _learningTrackRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLearningTrackCommandHandler(ILearningTrackRepository learningTrackRepository, IUnitOfWork unitOfWork)
    {
        _learningTrackRepository = learningTrackRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteLearningTrackCommand request, CancellationToken cancellationToken)
    {
        var learningTrack = await _learningTrackRepository.GetByIdAsync(request.Id, cancellationToken);

        if (learningTrack is null)
            return Result.Fail(LearningTrackErrors.NotFound);

        learningTrack.Deactivate();

        _learningTrackRepository.Update(learningTrack);

        var deleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!deleted)
            return Result.Fail(LearningTrackErrors.CannotBeDeleted);

        return Result.Ok();
    }
}
