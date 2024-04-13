using MediatR;

using AcademyHub.Common.Results;

namespace AcademyHub.Application.Users.CreateUser;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string Cpf,
    string Email,
    string Telephone,
    string Password,
    string PasswordCheck) : IRequest<Result<Guid>>;