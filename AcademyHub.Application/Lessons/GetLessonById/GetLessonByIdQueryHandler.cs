using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Domain.Lessons;
using AcademyHub.Application.Lessons.Models;

namespace AcademyHub.Application.Lessons.GetLessonById;

public sealed class GetLessonByIdQueryHandler : IRequestHandler<GetLessonByIdQuery, Result<LessonDetailsViewModel?>>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IMapper _mapper;

    public GetLessonByIdQueryHandler(ILessonRepository lessonRepository, IMapper mapper)
    {
        _lessonRepository = lessonRepository;
        _mapper = mapper;
    }

    public async Task<Result<LessonDetailsViewModel?>> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.Id, cancellationToken);

        if (lesson is null)
            return Result.Fail<LessonDetailsViewModel?>(LessonErrors.NotFound);

        var lessonViewModel = _mapper.Map<LessonDetailsViewModel?>(lesson);

        return Result.Ok(lessonViewModel);
    }
}