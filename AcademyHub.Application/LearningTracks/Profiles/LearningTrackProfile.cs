using AutoMapper;

using AcademyHub.Domain.LearningTracks;
using AcademyHub.Application.LearningTracks.Models;
using AcademyHub.Application.LearningTracks.UpdateLearningTrack;

namespace AcademyHub.Application.LearningTracks.Profiles;

internal sealed class LearningTrackProfile : Profile
{
    public LearningTrackProfile()
    {
        CreateMap<LearningTrack, LearningTrackDetailsViewModel>();
        CreateMap<LearningTrack, LearningTrackViewModel>();
        CreateMap<UpdateLearningTrackInputModel, UpdateLearningTrackCommand>();
    }
}