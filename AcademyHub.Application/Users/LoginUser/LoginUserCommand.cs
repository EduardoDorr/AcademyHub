using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Users.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : IRequest<Result<LoginUserViewModel?>>;