using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Common.Services;
using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Domain.Lessons;
using AcademyHub.Domain.CourseModules;

namespace AcademyHub.Application.CourseModules.AddLesson;

public sealed class AddLessonCommandHandler : IRequestHandler<AddLessonCommand, Result>
{
    ICourseModuleRepository _courseModuleRepository;
    ILessonRepository _lessonRepository;
    IUnitOfWork _unitOfWork;

    public AddLessonCommandHandler(ICourseModuleRepository courseModuleRepository, ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    {
        _courseModuleRepository = courseModuleRepository;
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddLessonCommand request, CancellationToken cancellationToken)
    {
        var courseModule = await _courseModuleRepository.GetByIdAsync(request.CourseModuleId, cancellationToken);

        if (courseModule is null)
            return Result.Fail(CourseModuleErrors.NotFound);

        var lessonsResult = await RepositoryExtension
            .GetEntitiesByIdAsync(_lessonRepository, request.LessonsId, cancellationToken);

        if (!lessonsResult.Success)
            return Result.Fail(lessonsResult.Errors);

        courseModule.AddLessons(lessonsResult.Value);

        _courseModuleRepository.Update(courseModule);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(CourseModuleErrors.CannotBeUpdated);

        return Result.Ok();
    }
}