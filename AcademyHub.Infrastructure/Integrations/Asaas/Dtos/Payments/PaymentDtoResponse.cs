using System.Text.Json.Serialization;

using AcademyHub.Application.Abstractions.Models;

namespace AcademyHub.Infrastructure.Integrations.Asaas.Dtos.Payments;

internal sealed class PaymentDtoResponse
{
    [JsonPropertyName("object")]
    public string? @object { get; set; }
    public string? id { get; set; }
    public string? dateCreated { get; set; }
    public string? customer { get; set; }
    public string? paymentLink { get; set; }
    public int value { get; set; }
    public float netValue { get; set; }
    public string? originalValue { get; set; }
    public string? interestValue { get; set; }
    public string? description { get; set; }
    public string? billingType { get; set; }
    public string? pixTransaction { get; set; }
    public string? status { get; set; }
    public string? dueDate { get; set; }
    public string? originalDueDate { get; set; }
    public string? paymentDate { get; set; }
    public string? clientPaymentDate { get; set; }
    public string? installmentNumber { get; set; }
    public string? invoiceUrl { get; set; }
    public string? invoiceNumber { get; set; }
    public string? externalReference { get; set; }
    public bool deleted { get; set; }
    public bool anticipated { get; set; }
    public bool anticipable { get; set; }
    public string? creditDate { get; set; }
    public string? estimatedCreditDate { get; set; }
    public string? transactionReceiptUrl { get; set; }
    public string? nossoNumero { get; set; }
    public string? bankSlipUrl { get; set; }
    public string? lastInvoiceViewedDate { get; set; }
    public string? lastBankSlipViewedDate { get; set; }
    public Discount? discount { get; set; }
    public Fine? fine { get; set; }
    public Interest? interest { get; set; }
    public bool postalService { get; set; }
    public string? custody { get; set; }
    public string? refunds { get; set; }
}

internal sealed class Discount
{
    public decimal value { get; set; }
    public string? limitDate { get; set; }
    public int dueDateLimitDays { get; set; }
    public string? type { get; set; }
}

internal sealed class Fine
{
    public decimal value { get; set; }
    public string? type { get; set; }
}

internal sealed class Interest
{
    public decimal value { get; set; }
    public string? type { get; set; }
}

internal static class PaymentDtoResponseExtension
{
    internal static CreatedPaymentModel ToModel(this PaymentDtoResponse response) =>
        new(response.id, response.invoiceUrl);
}