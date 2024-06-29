using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Domain.LessonFinisheds;
using AcademyHub.Application.LessonFinisheds.Models;

namespace AcademyHub.Application.LessonFinisheds.GetLessonsFinished;

public sealed class GetLessonsFinishedQueryHandler : IRequestHandler<GetLessonsFinishedQuery, Result<PaginationResult<LessonFinishedViewModel>>>
{
    private readonly ILessonFinishedRepository _lessonFinishedRepository;
    private readonly IMapper _mapper;

    public GetLessonsFinishedQueryHandler(ILessonFinishedRepository lessonFinishedRepository, IMapper mapper)
    {
        _lessonFinishedRepository = lessonFinishedRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginationResult<LessonFinishedViewModel>>> Handle(GetLessonsFinishedQuery request, CancellationToken cancellationToken)
    {
        var paginationLessonFinishedsFinished = await _lessonFinishedRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var lessonsViewModel = _mapper.Map<List<LessonFinishedViewModel>>(paginationLessonFinishedsFinished.Data);

        var paginationLessonFinishedsFinishedViewModel = paginationLessonFinishedsFinished.Map(lessonsViewModel);

        return Result.Ok(paginationLessonFinishedsFinishedViewModel);
    }
}