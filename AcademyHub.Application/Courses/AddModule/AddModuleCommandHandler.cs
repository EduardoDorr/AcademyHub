using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Services;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Courses;
using AcademyHub.Domain.CourseModules;

namespace AcademyHub.Application.Courses.AddModule;

public sealed class AddCourseModuleCommandHandler : IRequestHandler<AddCourseModuleCommand, Result>
{
    ICourseRepository _courseRepository;
    ICourseModuleRepository _courseModuleRepository;
    IUnitOfWork _unitOfWork;

    public AddCourseModuleCommandHandler(ICourseRepository courseRepository, ICourseModuleRepository courseModuleRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _courseModuleRepository = courseModuleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddCourseModuleCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);

        if (course is null)
            return Result.Fail(CourseErrors.NotFound);

        var modulesResult = await RepositoryExtension
            .GetEntitiesByIdAsync(_courseModuleRepository, request.CourseModulesId, cancellationToken);

        if (!modulesResult.Success)
            return Result.Fail(modulesResult.Errors);

        course.AddModules(modulesResult.Value);

        _courseRepository.Update(course);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(CourseErrors.CannotBeUpdated);

        return Result.Ok();
    }
}