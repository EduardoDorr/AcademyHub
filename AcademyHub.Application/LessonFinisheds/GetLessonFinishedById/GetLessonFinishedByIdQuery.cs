using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Application.LessonFinisheds.Models;

namespace AcademyHub.Application.LessonFinisheds.GetLessonFinishedById;

public sealed record GetLessonFinishedByIdQuery(Guid Id) : IRequest<Result<LessonFinishedDetailsViewModel?>>;