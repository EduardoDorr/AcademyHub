﻿using AcademyHub.Application.Courses.Models;

namespace AcademyHub.Application.LearningTracks.Models;

public sealed record LearningTrackDetailsViewModel(
    Guid Id,
    string Name,
    string Description,
    string? Cover,
    int Duration,
    IReadOnlyCollection<CourseDetailsViewModel> Courses);