using AutoMapper;

using AcademyHub.Domain.CourseModules;
using AcademyHub.Application.CourseModules.Models;
using AcademyHub.Application.CourseModules.UpdateCourseModule;

namespace AcademyHub.Application.CourseModules.Profiles;

internal sealed class CourseModuleProfile : Profile
{
    public CourseModuleProfile()
    {
        CreateMap<CourseModule, CourseModuleDetailsViewModel>()
            .ForCtorParam("Duration",
                          opt => opt.MapFrom(src => src.Lessons.Sum(lesson => lesson.Duration)));
        CreateMap<CourseModule, CourseModuleViewModel>();
        CreateMap<UpdateCourseModuleInputModel, UpdateCourseModuleCommand>();
    }
}