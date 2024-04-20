using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Services;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Courses;
using AcademyHub.Domain.LearningTracks;

namespace AcademyHub.Application.LearningTracks.AddCourse;

public sealed class AddCourseCommandHandler : IRequestHandler<AddCourseCommand, Result>
{
    ILearningTrackRepository _learningTrackRepository;
    ICourseRepository _courseRepository;
    IUnitOfWork _unitOfWork;

    public AddCourseCommandHandler(ILearningTrackRepository learningTrackRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _learningTrackRepository = learningTrackRepository;
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddCourseCommand request, CancellationToken cancellationToken)
    {
        var learningTrack = await _learningTrackRepository.GetByIdAsync(request.LearningTrackId, cancellationToken);

        if (learningTrack is null)
            return Result.Fail(LearningTrackErrors.NotFound);

        var coursesResult = await RepositoryExtension
            .GetEntitiesByIdAsync(_courseRepository, request.CoursesId, cancellationToken);

        if (!coursesResult.Success)
            return Result.Fail(coursesResult.Errors);

        learningTrack.AddCourses(coursesResult.Value);

        _learningTrackRepository.Update(learningTrack);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(LearningTrackErrors.CannotBeUpdated);

        return Result.Ok();
    }
}