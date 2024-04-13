using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Domain.Courses;
using AcademyHub.Application.Courses.Models;

namespace AcademyHub.Application.Courses.GetCourseById;

public sealed class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Result<CourseDetailsViewModel?>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public GetCourseByIdQueryHandler(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<Result<CourseDetailsViewModel?>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (course is null)
            return Result.Fail<CourseDetailsViewModel?>(CourseErrors.NotFound);

        var courseViewModel = _mapper.Map<CourseDetailsViewModel?>(course);

        return Result.Ok(courseViewModel);
    }
}