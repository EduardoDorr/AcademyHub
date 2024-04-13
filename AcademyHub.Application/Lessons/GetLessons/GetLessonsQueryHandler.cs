using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Domain.Lessons;
using AcademyHub.Application.Lessons.Models;

namespace AcademyHub.Application.Lessons.GetLessons;

public sealed class GetLessonsQueryHandler : IRequestHandler<GetLessonsQuery, Result<PaginationResult<LessonViewModel>>>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IMapper _mapper;

    public GetLessonsQueryHandler(ILessonRepository lessonRepository, IMapper mapper)
    {
        _lessonRepository = lessonRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginationResult<LessonViewModel>>> Handle(GetLessonsQuery request, CancellationToken cancellationToken)
    {
        var paginationLessons = await _lessonRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var lessonsViewModel = _mapper.Map<List<LessonViewModel>>(paginationLessons.Data);

        var paginationLessonsViewModel = paginationLessons.Map(lessonsViewModel);

        return Result.Ok(paginationLessonsViewModel);
    }
}