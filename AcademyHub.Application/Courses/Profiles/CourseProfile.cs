using AutoMapper;

using AcademyHub.Domain.Courses;
using AcademyHub.Application.Courses.Models;
using AcademyHub.Application.Courses.UpdateCourse;

namespace AcademyHub.Application.Courses.Profiles;

internal sealed class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseDetailsViewModel>();
        CreateMap<Course, CourseViewModel>();
        CreateMap<UpdateCourseInputModel, UpdateCourseCommand>();
    }
}