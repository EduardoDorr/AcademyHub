namespace AcademyHub.Application.Users.Models;

public sealed record UserDetailsViewModel(
    Guid Id,
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string Cpf,
    string Email,
    string Telephone,
    string Role);