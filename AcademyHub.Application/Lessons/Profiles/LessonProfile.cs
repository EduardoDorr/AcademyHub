using AutoMapper;

using AcademyHub.Domain.Lessons;
using AcademyHub.Application.Lessons.Models;
using AcademyHub.Application.Lessons.UpdateLesson;

namespace AcademyHub.Application.Lessons.Profiles;

internal sealed class LessonProfile : Profile
{
    public LessonProfile()
    {
        CreateMap<Lesson, LessonDetailsViewModel>();
        CreateMap<Lesson, LessonViewModel>();
        CreateMap<UpdateLessonInputModel, UpdateLessonCommand>();
    }
}