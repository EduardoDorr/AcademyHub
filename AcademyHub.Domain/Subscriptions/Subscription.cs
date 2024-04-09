using AcademyHub.Common.Results;
using AcademyHub.Common.Entities;
using AcademyHub.Domain.Enrollments;
using AcademyHub.Domain.LearningTracks;

namespace AcademyHub.Domain.Subscriptions;

public sealed class Subscription : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Duration { get; private set; }

    public List<Enrollment> Enrollments { get; private set; } = [];
    public List<LearningTrack> LearningTracks { get; private set; } = [];

    protected Subscription() { }

    private Subscription(
        string name,
        string description,
        int duration)
    {
        Name = name;
        Description = description;
        Duration = duration;
    }

    public static Result<Subscription> Create(
        string name,
        string description,
        int duration)
    {
        var subscription =
            new Subscription(
                name,
                description,
                duration);

        return Result<Subscription>.Ok(subscription);
    }

    public void Update(string name, string description, int duration)
    {
        Name = name;
        Description = description;
        Duration = duration;
    }

    public void AddLearningTracks(IList<LearningTrack>? learningTracks)
    {
        if (learningTracks is null)
            return;

        LearningTracks.AddRange(learningTracks);
    }

    public void RemoveLearningTracks(IList<LearningTrack>? learningTracks)
    {
        if (learningTracks is null)
            return;

        foreach (var learningTrack in learningTracks)
            LearningTracks.Single(s => s == learningTrack).Deactivate();
    }
}