using MediatR;

using AcademyHub.Common.Results;
using AcademyHub.Application.Users.Models;

namespace AcademyHub.Application.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDetailsViewModel?>>;