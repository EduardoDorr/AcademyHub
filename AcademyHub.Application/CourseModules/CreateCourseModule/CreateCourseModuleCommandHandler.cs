using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.CourseModules;

namespace AcademyHub.Application.CourseModules.CreateCourseModule;

public sealed class CreateCourseModuleCommandHandler : IRequestHandler<CreateCourseModuleCommand, Result<Guid>>
{
    private readonly ICourseModuleRepository _courseModuleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseModuleCommandHandler(ICourseModuleRepository courseModuleRepository, IUnitOfWork unitOfWork)
    {
        _courseModuleRepository = courseModuleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCourseModuleCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _courseModuleRepository.IsUniqueAsync(request.Name, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(CourseModuleErrors.IsNotUnique);

        var courseModuleResult = CourseModule.Create(
            request.Name,
            request.Description);

        if (!courseModuleResult.Success)
            return Result.Fail<Guid>(courseModuleResult.Errors);

        _courseModuleRepository.Create(courseModuleResult.Value);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(CourseModuleErrors.CannotBeCreated);

        return Result.Ok(courseModuleResult.Value.Id);
    }
}