using System.Text.Json.Serialization;

using AcademyHub.Domain.EnrollmentPayments;

namespace AcademyHub.Infrastructure.Integrations.Asaas.Dtos.Payments;

public class PaymentWebhookEvent
{
    public string id { get; set; }
    [JsonPropertyName("event")]
    public string @event { get; set; }
    public PaymentDtoResponse payment { get; set; }
}

public enum PaymentWebhookEventType
{
    PAYMENT_CONFIRMED = 1,
    PAYMENT_DELETED = 2,
    PAYMENT_UPDATED = 3,
    PAYMENT_RECEIVED = 4,
    PAYMENT_OVERDUE = 5
}

public static class PaymentWebhookEventExtensions
{
    public static EnrollmentPaymentStatus ToEnrollmentPaymentStatus(this PaymentWebhookEventType type)
    {
        switch (type)
        {
            case PaymentWebhookEventType.PAYMENT_CONFIRMED:
            case PaymentWebhookEventType.PAYMENT_RECEIVED:
                return EnrollmentPaymentStatus.Success;
            case PaymentWebhookEventType.PAYMENT_OVERDUE:
                return EnrollmentPaymentStatus.Overdue;
            case PaymentWebhookEventType.PAYMENT_DELETED:
                return EnrollmentPaymentStatus.Fail;
            case PaymentWebhookEventType.PAYMENT_UPDATED:
                return EnrollmentPaymentStatus.Pending;
            default:
                return EnrollmentPaymentStatus.Pending;
        }
    }
}