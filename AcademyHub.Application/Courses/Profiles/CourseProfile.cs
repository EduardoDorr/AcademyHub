using AutoMapper;

using AcademyHub.Domain.Courses;
using AcademyHub.Application.Courses.Models;
using AcademyHub.Application.Courses.UpdateCourse;

namespace AcademyHub.Application.Courses.Profiles;

internal sealed class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseDetailsViewModel>()
            .ForCtorParam("Duration", 
                          opt => opt.MapFrom(src => src.CourseModules.Sum(m => m.Lessons.Sum(lesson => lesson.Duration))));
        CreateMap<Course, CourseViewModel>();
        CreateMap<UpdateCourseInputModel, UpdateCourseCommand>();
    }
}