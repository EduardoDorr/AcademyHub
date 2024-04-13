using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Users.DeleteUser;

public sealed record DeleteUserCommand(Guid Id) : IRequest<Result>;