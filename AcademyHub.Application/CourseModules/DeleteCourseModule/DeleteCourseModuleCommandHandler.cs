using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.CourseModules;

namespace AcademyHub.Application.CourseModules.DeleteCourseModule;

public sealed class DeleteCourseModuleCommandHandler : IRequestHandler<DeleteCourseModuleCommand, Result>
{
    private readonly ICourseModuleRepository _courseModuleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseModuleCommandHandler(ICourseModuleRepository courseModuleRepository, IUnitOfWork unitOfWork)
    {
        _courseModuleRepository = courseModuleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteCourseModuleCommand request, CancellationToken cancellationToken)
    {
        var courseModule = await _courseModuleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (courseModule is null)
            return Result.Fail(CourseModuleErrors.NotFound);

        courseModule.Deactivate();

        _courseModuleRepository.Update(courseModule);

        var deleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!deleted)
            return Result.Fail(CourseModuleErrors.CannotBeDeleted);

        return Result.Ok();
    }
}
