using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.LearningTracks.AddCourse;

public sealed record AddCourseCommand(
    Guid LearningTrackId,
    IList<Guid> CoursesId) : IRequest<Result>;