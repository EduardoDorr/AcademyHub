namespace AcademyHub.Application.LearningTracks.Models;

public sealed record LearningTrackViewModel(
    string Name,
    string Description,
    string? Cover);