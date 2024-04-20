using AcademyHub.Application.LearningTracks.Models;

namespace AcademyHub.Application.Subscriptions.Models;

public sealed record SubscriptionDetailsViewModel(
    Guid Id,
    string Name,
    string Description,
    int Duration,
    IReadOnlyCollection<LearningTrackDetailsViewModel> LearningTracks);