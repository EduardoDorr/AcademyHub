using AcademyHub.Application.Abstractions.Models;
using System.Text.Json.Serialization;

namespace AcademyHub.Infrastructure.Integrations.Asaas.Dtos.Customers;

internal sealed record CustomerDtoRequest(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("cpfCnpj")] string CpfCnpj,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("mobilePhone")] string MobilePhone,
    [property: JsonPropertyName("externalReference")] string ExternalReference);

internal static class CustomerDtoRequestExtension
{
    internal static CustomerDtoRequest FromModel(this CustomerModel model) =>
        new(model.Name,
            model.Cpf,
            model.Email,
            model.Telephone,
            model.Id.ToString());
}