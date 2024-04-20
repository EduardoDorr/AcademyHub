using AcademyHub.Application.Abstractions.Models;

namespace AcademyHub.Infrastructure.Integrations.Asaas.Dtos.Customers;

internal sealed record CustomerDtoRequest(
    string Name,
    string CpfCnpj,
    string Email,
    string MobilePhone,
    string ExternalReference);

internal static class CustomerDtoRequestExtension
{
    internal static CustomerDtoRequest FromModel(this CustomerModel model) =>
        new(model.Name,
            model.Cpf,
            model.Email,
            model.Telephone,
            model.Id.ToString());
}