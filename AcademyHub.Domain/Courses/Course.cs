using AcademyHub.Common.Results;
using AcademyHub.Common.Entities;

using AcademyHub.Domain.CourseModules;
using AcademyHub.Domain.LearningTracks;

namespace AcademyHub.Domain.Courses;

public sealed class Course : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string? Cover { get; private set; }

    public List<CourseModule> CourseModules { get; private set; } = [];
    public List<LearningTrack> LearningTracks { get; private set; } = [];

    protected Course() { }

    private Course(
        string name,
        string description,
        string? cover)
    {
        Name = name;
        Description = description;
        Cover = cover;
    }

    public static Result<Course> Create(
        string name,
        string description,
        string? cover)
    {
        var course =
            new Course(
                name,
                description,
                cover);

        return Result<Course>.Ok(course);
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void UploadCover(string cover)
    {
        Cover = cover;
    }

    public void AddModules(IList<CourseModule>? courseModules)
    {
        if (courseModules is null)
            return;

        CourseModules.AddRange(courseModules);
    }

    public void RemoveModules(IList<CourseModule>? courseModules)
    {
        if (courseModules is null)
            return;

        foreach (var courseModule in courseModules)
            CourseModules.Single(s => s == courseModule).Deactivate();
    }
}