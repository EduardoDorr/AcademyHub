namespace AcademyHub.Infrastructure.Integrations.Asaas.Dtos.Payments;

internal sealed record PaymentDtoRequest(
    string Customer,
    string BillingType,
    decimal Value,
    string DueDate,
    string Description);
