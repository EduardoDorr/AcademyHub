using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Services;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.CourseModules;
using AcademyHub.Domain.Courses;

namespace AcademyHub.Application.Courses.RemoveModule;

public sealed class RemoveCourseModuleCommandHandler : IRequestHandler<RemoveCourseModuleCommand, Result>
{
    ICourseRepository _courseRepository;
    ICourseModuleRepository _courseModuleRepository;
    IUnitOfWork _unitOfWork;

    public RemoveCourseModuleCommandHandler(ICourseRepository courseRepository, ICourseModuleRepository courseModuleRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _courseModuleRepository = courseModuleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveCourseModuleCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);

        if (course is null)
            return Result.Fail(CourseErrors.NotFound);

        var modulesResult = await RepositoryExtension
            .GetEntitiesByIdAsync(_courseModuleRepository, request.CourseModulesId, cancellationToken);

        if (!modulesResult.Success)
            return Result.Fail(modulesResult.Errors);

        course.RemoveModules(modulesResult.Value);

        _courseRepository.Update(course);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(CourseErrors.CannotBeUpdated);

        return Result.Ok();
    }
}