using AcademyHub.Common.Results;
using AcademyHub.Common.Entities;
using AcademyHub.Domain.Courses;
using AcademyHub.Domain.Subscriptions;

namespace AcademyHub.Domain.LearningTracks;

public sealed class LearningTrack : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string? Cover { get; private set; }

    public List<Course> Courses { get; private set; } = [];
    public List<Subscription> Subscriptions { get; private set; } = [];

    protected LearningTrack() { }

    private LearningTrack(string name, string description, string? cover)
    {
        Name = name;
        Description = description;
        Cover = cover;
    }

    public static Result<LearningTrack> Create(
        string name,
        string description,
        string? cover)
    {
        var learningTrack =
            new LearningTrack(
                name,
                description,
                cover);

        return Result<LearningTrack>.Ok(learningTrack);
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

    public void AddCourses(IList<Course>? courses)
    {
        if (courses is null)
            return;

        Courses.AddRange(courses);
    }

    public void RemoveCourses(IList<Course>? courses)
    {
        if (courses is null)
            return;

        foreach (var course in courses)
            Courses.Remove(course);
    }
}