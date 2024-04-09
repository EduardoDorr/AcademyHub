using AcademyHub.Common.Results;
using AcademyHub.Common.Entities;
using AcademyHub.Domain.CourseModules;
using AcademyHub.Domain.LessonFinisheds;

namespace AcademyHub.Domain.Lessons;

public sealed class Lesson : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string VideoLink { get; private set; }
    public int Duration { get; private set; }
    public decimal AverageRating { get; private set; }
    public int NumberOfRatings { get; private set; }

    public List<LessonFinished> LessonFinisheds { get; private set; }
    public List<CourseModule> CourseModules { get; private set; }

    protected Lesson() { }

    private Lesson(
        string name,
        string description,
        string videoLink,
        int duration)
    {
        Name = name;
        Description = description;
        VideoLink = videoLink;
        Duration = duration;
    }

    public static Result<Lesson> Create(
        string name,
        string description,
        string videoLink,
        int duration)
    {
        var courseModule =
            new Lesson(
                name,
                description,
                videoLink,
                duration);

        return Result.Ok(courseModule);
    }

    public void Update(string name, string description, string videoLink, int duration)
    {
        Name = name;
        Description = description;
        VideoLink = videoLink;
        Duration = duration;
    }

    public void UpdateRating(decimal averageRating, int numberOfRatings)
    {
        AverageRating = averageRating;
        NumberOfRatings = numberOfRatings;
    }
}