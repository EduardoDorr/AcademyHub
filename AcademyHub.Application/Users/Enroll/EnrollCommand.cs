using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Users.Enroll;

public sealed record EnrollCommand(
    Guid UserId,
    Guid SubscriptionId,
    decimal Value) : IRequest<Result<Guid>>;