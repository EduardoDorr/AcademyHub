using System.Text.Json.Serialization;
using AcademyHub.Application.Abstractions.Models;

namespace AcademyHub.Infrastructure.Integrations.Asaas.Dtos.Customers;

internal sealed record CustomerDtoResponse(
    [property: JsonPropertyName("object")] string? @object,
    string? id,
    string? dateCreated,
    string? name,
    string? email,
    string? company,
    string? phone,
    string? mobilePhone,
    string? address,
    string? addressNumber,
    string? complement,
    string? province,
    string? postalCode,
    string? cpfCnpj,
    string? personType,
    bool deleted,
    string? additionalEmails,
    string? externalReference,
    bool notificationDisabled,
    string? observations,
    string? municipalInscription,
    string? stateInscription,
    bool canDelete,
    string? cannotBeDeletedReason,
    bool canEdit,
    string? cannotEditReason,
    string? city,
    string? cityName,
    string? state,
    string? country);

internal static class CustomerDtoResponseExtension
{
    internal static CustomerModel ToModel(this CustomerDtoResponse? dto) =>
        new(new Guid(dto.externalReference),
            dto.name,
            dto.cpfCnpj,
            dto.email,
            dto.mobilePhone);
}