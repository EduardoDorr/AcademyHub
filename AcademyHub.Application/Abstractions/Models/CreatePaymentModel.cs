namespace AcademyHub.Application.Abstractions.Models;

public sealed record CreatePaymentModel(
    string CustomerId,
    decimal Value,
    DateTime DueDate,
    string Description);