using AcademyHub.Common.Results;
using AcademyHub.Application.Abstractions.Models;

namespace AcademyHub.Application.Abstractions.PaymentGateway;

public interface IPaymentGateway
{
    Task<Result<string?>> CreateClientAsync(CustomerModel model);
    Task<Result<CustomerModel?>> GetClientAsync(string id);
    Task<Result<CreatedPaymentModel?>> CreatePaymentAsync(CreatePaymentModel model);
}