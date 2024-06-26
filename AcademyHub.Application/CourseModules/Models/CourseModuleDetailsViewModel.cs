﻿using AcademyHub.Application.Lessons.Models;

namespace AcademyHub.Application.CourseModules.Models;

public sealed record CourseModuleDetailsViewModel(
    Guid Id,
    string Name,
    string Description,
    int Duration,
    IReadOnlyCollection<LessonDetailsViewModel> Lessons);