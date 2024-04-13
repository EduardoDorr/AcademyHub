using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Application.Lessons.Models;

namespace AcademyHub.Application.Lessons.GetLessonById;

public sealed record GetLessonByIdQuery(Guid Id) : IRequest<Result<LessonDetailsViewModel?>>;