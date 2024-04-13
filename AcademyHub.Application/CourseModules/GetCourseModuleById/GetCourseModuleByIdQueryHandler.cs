using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Domain.CourseModules;
using AcademyHub.Application.CourseModules.Models;

namespace AcademyHub.Application.CourseModules.GetCourseModuleById;

public sealed class GetCourseModuleByIdQueryHandler : IRequestHandler<GetCourseModuleByIdQuery, Result<CourseModuleDetailsViewModel?>>
{
    private readonly ICourseModuleRepository _courseModuleRepository;
    private readonly IMapper _mapper;

    public GetCourseModuleByIdQueryHandler(ICourseModuleRepository courseModuleRepository, IMapper mapper)
    {
        _courseModuleRepository = courseModuleRepository;
        _mapper = mapper;
    }

    public async Task<Result<CourseModuleDetailsViewModel?>> Handle(GetCourseModuleByIdQuery request, CancellationToken cancellationToken)
    {
        var courseModule = await _courseModuleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (courseModule is null)
            return Result.Fail<CourseModuleDetailsViewModel?>(CourseModuleErrors.NotFound);

        var courseModuleViewModel = _mapper.Map<CourseModuleDetailsViewModel?>(courseModule);

        return Result.Ok(courseModuleViewModel);
    }
}