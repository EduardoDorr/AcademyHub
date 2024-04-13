using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.CourseModules;

namespace AcademyHub.Application.CourseModules.UpdateCourseModule;

public sealed class UpdateCourseModuleCommandHandler : IRequestHandler<UpdateCourseModuleCommand, Result>
{
    private readonly ICourseModuleRepository _courseModuleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCourseModuleCommandHandler(ICourseModuleRepository courseModuleRepository, IUnitOfWork unitOfWork)
    {
        _courseModuleRepository = courseModuleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateCourseModuleCommand request, CancellationToken cancellationToken)
    {
        var courseModule = await _courseModuleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (courseModule is null)
            return Result.Fail(CourseModuleErrors.NotFound);

        courseModule.Update(
            request.Name,
            request.Description);
        ;

        _courseModuleRepository.Update(courseModule);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(CourseModuleErrors.CannotBeUpdated);

        return Result.Ok();
    }
}