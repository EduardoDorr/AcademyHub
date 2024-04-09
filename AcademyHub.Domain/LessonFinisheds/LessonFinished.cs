using AcademyHub.Common.Results;
using AcademyHub.Common.Entities;
using AcademyHub.Domain.Users;
using AcademyHub.Domain.Lessons;

namespace AcademyHub.Domain.LessonFinisheds;

public sealed class LessonFinished : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid LessonId { get; private set; }
    public LessonFinishedRating Rating { get; private set; }
    public string? Comment { get; private set; }
    public DateTime FinishDate { get; private set; }

    public User User { get; private set; }
    public Lesson Lesson { get; private set; }

    protected LessonFinished() { }

    private LessonFinished(
        Guid userId,
        Guid lessonId,
        LessonFinishedRating rating,
        string? comment)
    {
        UserId = userId;
        LessonId = lessonId;
        Rating = rating;
        Comment = comment;

        FinishDate = DateTime.Now;
    }

    public static Result<LessonFinished> Create(
        Guid userId,
        Guid lessonId,
        LessonFinishedRating rating,
        string? comment)
    {
        var lessonFinished =
            new LessonFinished(
                userId,
                lessonId,
                rating,
                comment);

        return Result.Ok(lessonFinished);
    }

    public void Update(LessonFinishedRating rating, string? comment)
    {
        Rating = rating;
        Comment = comment;
    }
}