using AutoMapper;

using AcademyHub.Domain.LearningTracks;
using AcademyHub.Application.LearningTracks.Models;
using AcademyHub.Application.LearningTracks.UpdateLearningTrack;

namespace AcademyHub.Application.LearningTracks.Profiles;

internal sealed class LearningTrackProfile : Profile
{
    public LearningTrackProfile()
    {
        CreateMap<LearningTrack, LearningTrackDetailsViewModel>()
            .ForCtorParam("Duration",
                          opt => opt.MapFrom(
                              src => src.Courses.Sum(
                                  c => c.CourseModules.Sum(
                                      m => m.Lessons.Sum(
                                          lesson => lesson.Duration)))));
        CreateMap<LearningTrack, LearningTrackViewModel>();
        CreateMap<UpdateLearningTrackInputModel, UpdateLearningTrackCommand>();
    }
}