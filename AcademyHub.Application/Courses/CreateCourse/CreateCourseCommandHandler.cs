using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Courses;

namespace AcademyHub.Application.Courses.CreateCourse;

public sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<Guid>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _courseRepository.IsUniqueAsync(request.Name, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(CourseErrors.IsNotUnique);

        var courseResult = Course.Create(
            request.Name,
            request.Description,
            request.Cover);

        if (!courseResult.Success)
            return Result.Fail<Guid>(courseResult.Errors);

        _courseRepository.Create(courseResult.Value);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(CourseErrors.CannotBeCreated);

        return Result.Ok(courseResult.Value.Id);
    }
}