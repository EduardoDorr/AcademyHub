using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Common.Models.Pagination;
using AcademyHub.Domain.LearningTracks;
using AcademyHub.Application.LearningTracks.Models;

namespace AcademyHub.Application.LearningTracks.GetLearningTracks;

public sealed class GetLearningTracksQueryHandler : IRequestHandler<GetLearningTracksQuery, Result<PaginationResult<LearningTrackViewModel>>>
{
    private readonly ILearningTrackRepository _learningTrackRepository;
    private readonly IMapper _mapper;

    public GetLearningTracksQueryHandler(ILearningTrackRepository learningTrackRepository, IMapper mapper)
    {
        _learningTrackRepository = learningTrackRepository;
        _mapper = mapper;
    }

    public async Task<Result<PaginationResult<LearningTrackViewModel>>> Handle(GetLearningTracksQuery request, CancellationToken cancellationToken)
    {
        var paginationLearningTracks = await _learningTrackRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var learningTracksViewModel = _mapper.Map<List<LearningTrackViewModel>>(paginationLearningTracks.Data);

        var paginationLearningTracksViewModel = paginationLearningTracks.Map(learningTracksViewModel);

        return Result.Ok(paginationLearningTracksViewModel);
    }
}