using System.Text.Json;

using Microsoft.Extensions.Options;

using AcademyHub.Common.Options;
using AcademyHub.Common.Results;

using AcademyHub.Application.Abstractions.Models;
using AcademyHub.Application.Abstractions.PaymentGateway;

using AcademyHub.Infrastructure.Integrations.Api;
using AcademyHub.Infrastructure.Integrations.Asaas.Dtos.Errors;
using AcademyHub.Infrastructure.Integrations.Asaas.Dtos.Payments;
using AcademyHub.Infrastructure.Integrations.Asaas.Dtos.Customers;

namespace AcademyHub.Infrastructure.Integrations.Asaas.Apis;

internal class AsaasPaymentGatewayApi : IPaymentGateway
{
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly string _customerEndpoint;
    private readonly string _paymentEndpoint;
    private readonly Dictionary<string, string> _headers;

    public AsaasPaymentGatewayApi(IOptions<AsaasApiOptions> asaasApiOptions)
    {
        _apiKey = asaasApiOptions.Value.ApiKey;
        _baseUrl = asaasApiOptions.Value.BaseUrl;
        _baseUrl = asaasApiOptions.Value.CustomerEndpoint;
        _baseUrl = asaasApiOptions.Value.PaymentEndpoint;

        _headers = new Dictionary<string, string>
        {
            { "access_token", _apiKey }
        };
    }

    public async Task<Result<string?>> CreateClientAsync(CustomerModel model)
    {
        var json = JsonSerializer.Serialize(model);

        var response = await RestHelper
            .PostAsync<CustomerDtoResponse>(
                apiUrl: _baseUrl,
                requestUri: _customerEndpoint,
                headerParameters: _headers,
                json: json);

        if (response.IsSuccessStatusCode)
            return Result.Ok(response.Data.id);

        var error = JsonSerializer.Deserialize<ErrorDtoResponse?>(response.Content);

        return Result.Fail<string?>(error.ToError());
    }

    public async Task<Result<CustomerModel?>> GetClientAsync(string id)
    {
        var response = await RestHelper
            .GetAsync<CustomerDtoResponse>(
                apiUrl: _baseUrl,
                requestUri: $"{_customerEndpoint}/{id}",
                headerParameters: _headers);

        if (response.IsSuccessStatusCode)
            return Result.Ok(response.Data.ToModel());

        var error = JsonSerializer.Deserialize<ErrorDtoResponse?>(response.Content);

        return Result.Fail<CustomerModel?>(error.ToError());
    }

    public async Task<Result<CreatedPaymentModel?>> CreatePaymentAsync(CreatePaymentModel model)
    {
        var json = JsonSerializer.Serialize(model);

        var response = await RestHelper
            .PostAsync<PaymentDtoResponse>(
                apiUrl: _baseUrl,
                requestUri: _paymentEndpoint,
                headerParameters: _headers,
                json: json);

        if (response.IsSuccessStatusCode)
            return Result.Ok(response.Data.ToModel());

        var error = JsonSerializer.Deserialize<ErrorDtoResponse?>(response.Content);

        return Result.Fail<CreatedPaymentModel?>(error.ToError());
    }
}
