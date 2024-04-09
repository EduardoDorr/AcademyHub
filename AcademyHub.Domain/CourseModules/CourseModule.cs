using AcademyHub.Common.Results;
using AcademyHub.Common.Entities;
using AcademyHub.Domain.Lessons;
using AcademyHub.Domain.Courses;

namespace AcademyHub.Domain.CourseModules;

public sealed class CourseModule : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public List<Lesson> Lessons { get; private set; } = [];
    public List<Course> Courses { get; private set; } = [];

    protected CourseModule() { }

    private CourseModule(
        string name,
        string description)
    {
        Name = name;
        Description = description;
    }

    public static Result<CourseModule> Create(
        string name,
        string description)
    {
        var courseModule =
            new CourseModule(
                name,
                description);

        return Result<CourseModule>.Ok(courseModule);
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddModules(IList<Lesson>? lessons)
    {
        if (lessons is null)
            return;

        Lessons.AddRange(lessons);
    }

    public void RemoveModules(IList<Lesson>? lessons)
    {
        if (lessons is null)
            return;

        foreach (var lesson in lessons)
            Lessons.Single(s => s == lesson).Deactivate();
    }
}