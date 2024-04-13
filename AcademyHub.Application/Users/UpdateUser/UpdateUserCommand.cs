using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string Email,
    string Telephone) : IRequest<Result>;