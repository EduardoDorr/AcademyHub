using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Domain.Courses;
using AcademyHub.Application.Courses.Models;

namespace AcademyHub.Application.Courses.GetCourses;

public sealed class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, Result<PaginationResult<CourseViewModel>>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public GetCoursesQueryHandler(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginationResult<CourseViewModel>>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var paginationCourses = await _courseRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var coursesViewModel = _mapper.Map<List<CourseViewModel>>(paginationCourses.Data);

        var paginationCoursesViewModel = paginationCourses.Map(coursesViewModel);

        return Result.Ok(paginationCoursesViewModel);
    }
}