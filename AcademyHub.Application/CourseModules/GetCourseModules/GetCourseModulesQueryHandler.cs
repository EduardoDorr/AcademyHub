using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Domain.CourseModules;
using AcademyHub.Application.CourseModules.Models;

namespace AcademyHub.Application.CourseModules.GetCourseModules;

public sealed class GetCourseModulesQueryHandler : IRequestHandler<GetCourseModulesQuery, Result<PaginationResult<CourseModuleViewModel>>>
{
    private readonly ICourseModuleRepository _courseModuleRepository;
    private readonly IMapper _mapper;

    public GetCourseModulesQueryHandler(ICourseModuleRepository courseModuleRepository, IMapper mapper)
    {
        _courseModuleRepository = courseModuleRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginationResult<CourseModuleViewModel>>> Handle(GetCourseModulesQuery request, CancellationToken cancellationToken)
    {
        var paginationCourseModules = await _courseModuleRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var courseModulesViewModel = _mapper.Map<List<CourseModuleViewModel>>(paginationCourseModules.Data);

        var paginationCourseModulesViewModel = paginationCourseModules.Map(courseModulesViewModel);

        return Result.Ok(paginationCourseModulesViewModel);
    }
}