namespace AcademyHub.Application.Users.UpdateUser;

public sealed record UpdateUserInputModel(
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string Email,
    string Telephone);