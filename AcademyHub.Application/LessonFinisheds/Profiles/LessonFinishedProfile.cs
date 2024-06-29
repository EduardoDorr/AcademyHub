using AutoMapper;

using AcademyHub.Domain.LessonFinisheds;
using AcademyHub.Application.LessonFinisheds.Models;
using AcademyHub.Application.LessonFinisheds.UpdateLessonFinished;

namespace AcademyHub.Application.LessonFinisheds.Profiles;

internal sealed class LessonFinishedProfile : Profile
{
    public LessonFinishedProfile()
    {
        CreateMap<LessonFinished, LessonFinishedDetailsViewModel>();
        CreateMap<LessonFinished, LessonFinishedViewModel>();
        CreateMap<UpdateLessonFinishedInputModel, UpdateLessonFinishedCommand>();
    }
}