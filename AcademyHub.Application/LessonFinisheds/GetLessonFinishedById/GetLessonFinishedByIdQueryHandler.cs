using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Domain.LessonFinisheds;
using AcademyHub.Application.LessonFinisheds.Models;

namespace AcademyHub.Application.LessonFinisheds.GetLessonFinishedById;

public sealed class GetLessonFinishedByIdQueryHandler : IRequestHandler<GetLessonFinishedByIdQuery, Result<LessonFinishedDetailsViewModel?>>
{
    private readonly ILessonFinishedRepository _lessonFinishedRepository;
    private readonly IMapper _mapper;

    public GetLessonFinishedByIdQueryHandler(ILessonFinishedRepository lessonFinishedRepository, IMapper mapper)
    {
        _lessonFinishedRepository = lessonFinishedRepository;
        _mapper = mapper;
    }

    public async Task<Result<LessonFinishedDetailsViewModel?>> Handle(GetLessonFinishedByIdQuery request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonFinishedRepository.GetByIdAsync(request.Id, cancellationToken);

        if (lesson is null)
            return Result.Fail<LessonFinishedDetailsViewModel?>(LessonFinishedErrors.NotFound);

        var lessonViewModel = _mapper.Map<LessonFinishedDetailsViewModel?>(lesson);

        return Result.Ok(lessonViewModel);
    }
}