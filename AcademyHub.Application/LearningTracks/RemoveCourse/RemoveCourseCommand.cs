using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.LearningTracks.RemoveCourse;

public sealed record RemoveCourseCommand(
    Guid LearningTrackId,
    IList<Guid> CoursesId) : IRequest<Result>;