using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Services;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Courses;
using AcademyHub.Domain.LearningTracks;

namespace AcademyHub.Application.LearningTracks.RemoveCourse;

public sealed class RemoveCourseCommandHandler : IRequestHandler<RemoveCourseCommand, Result>
{
    ILearningTrackRepository _learningTrackRepository;
    ICourseRepository _courseModuleRepository;
    IUnitOfWork _unitOfWork;

    public RemoveCourseCommandHandler(ILearningTrackRepository learningTrackRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _learningTrackRepository = learningTrackRepository;
        _courseModuleRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveCourseCommand request, CancellationToken cancellationToken)
    {
        var learningTrack = await _learningTrackRepository.GetByIdAsync(request.LearningTrackId, cancellationToken);

        if (learningTrack is null)
            return Result.Fail(LearningTrackErrors.NotFound);

        var modulesResult = await RepositoryExtension
            .GetEntitiesByIdAsync(_courseModuleRepository, request.CoursesId, cancellationToken);

        if (!modulesResult.Success)
            return Result.Fail(modulesResult.Errors);

        learningTrack.RemoveCourses(modulesResult.Value);

        _learningTrackRepository.Update(learningTrack);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(LearningTrackErrors.CannotBeUpdated);

        return Result.Ok();
    }
}