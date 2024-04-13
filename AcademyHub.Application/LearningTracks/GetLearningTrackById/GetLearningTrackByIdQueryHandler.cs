using MediatR;
using AutoMapper;

using AcademyHub.Common.Results;
using AcademyHub.Domain.LearningTracks;
using AcademyHub.Application.LearningTracks.Models;

namespace AcademyHub.Application.LearningTracks.GetLearningTrackById;

public sealed class GetLearningTrackByIdQueryHandler : IRequestHandler<GetLearningTrackByIdQuery, Result<LearningTrackDetailsViewModel?>>
{
    private readonly ILearningTrackRepository _learningTrackRepository;
    private readonly IMapper _mapper;

    public GetLearningTrackByIdQueryHandler(ILearningTrackRepository learningTrackRepository, IMapper mapper)
    {
        _learningTrackRepository = learningTrackRepository;
        _mapper = mapper;
    }

    public async Task<Result<LearningTrackDetailsViewModel?>> Handle(GetLearningTrackByIdQuery request, CancellationToken cancellationToken)
    {
        var learningTrack = await _learningTrackRepository.GetByIdAsync(request.Id, cancellationToken);

        if (learningTrack is null)
            return Result.Fail<LearningTrackDetailsViewModel?>(LearningTrackErrors.NotFound);

        var learningTrackViewModel = _mapper.Map<LearningTrackDetailsViewModel?>(learningTrack);

        return Result.Ok(learningTrackViewModel);
    }
}