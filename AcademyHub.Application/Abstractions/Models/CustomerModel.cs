using AcademyHub.Domain.Users;

namespace AcademyHub.Application.Abstractions.Models;

public sealed record CustomerModel(
    Guid Id,
    string Name,
    string Cpf,
    string Email,
    string Telephone);

public static class ClientModelExtension
{
    public static CustomerModel ToModel(this User user) =>
        new(user.Id,
            $"{user.FirstName} {user.LastName}",
            user.Cpf.Number,
            user.Email.Address,
            user.Telephone.Number);
}