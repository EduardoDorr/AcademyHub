using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Courses;

namespace AcademyHub.Application.Courses.DeleteCourse;

public sealed class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (course is null)
            return Result.Fail(CourseErrors.NotFound);

        course.Deactivate();

        _courseRepository.Update(course);

        var deleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!deleted)
            return Result.Fail(CourseErrors.CannotBeDeleted);

        return Result.Ok();
    }
}
