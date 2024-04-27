using System.Text.Json.Serialization;

using AcademyHub.Application.Abstractions.Models;

namespace AcademyHub.Infrastructure.Integrations.Asaas.Dtos.Payments;

internal sealed record PaymentDtoRequest(
    [property: JsonPropertyName("customer")] string Customer,
    [property: JsonPropertyName("billingType")] string BillingType,
    [property: JsonPropertyName("value")] decimal Value,
    [property: JsonPropertyName("dueDate")] string DueDate,
    [property: JsonPropertyName("description")] string Description);

internal static class PaymentDtoRequestExtension
{
    internal static PaymentDtoRequest FromModel(this CreatePaymentModel model) =>
        new(model.CustomerId,
            "UNDEFINED",
            model.Value,
            model.DueDate.ToString("yyyy-MM-dd"),
            model.Description);
}