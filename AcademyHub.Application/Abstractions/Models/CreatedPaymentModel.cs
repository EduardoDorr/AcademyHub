namespace AcademyHub.Application.Abstractions.Models;

public sealed record CreatedPaymentModel(
    string PaymentId,
    string InvoiceUrl); 