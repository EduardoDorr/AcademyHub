using RestSharp;

namespace AcademyHub.Infrastructure.Integrations.Api;

internal static class RestHelper
{
    public static async Task<RestResponse<T>> PostAsync<T>(string apiUrl, string requestUri, Dictionary<string, string>? headerParameters = null, Dictionary<string, string>? queryParameters = null, string? json = null)
    {
        return await ExecuteAsync<T>(Method.Post, apiUrl, requestUri, headerParameters, queryParameters, json);
    }

    public static async Task<RestResponse<T>> PutAsync<T>(string apiUrl, string requestUri, Dictionary<string, string>? headerParameters = null, Dictionary<string, string>? queryParameters = null, string? json = null)
    {
        return await ExecuteAsync<T>(Method.Put, apiUrl, requestUri, headerParameters, queryParameters, json);
    }

    public static async Task<RestResponse<T>> DeleteAsync<T>(string apiUrl, string requestUri, Dictionary<string, string>? headerParameters = null, Dictionary<string, string>? queryParameters = null, string? json = null)
    {
        return await ExecuteAsync<T>(Method.Delete, apiUrl, requestUri, headerParameters, queryParameters, json);
    }

    public static async Task<RestResponse<T>> GetAsync<T>(string apiUrl, string requestUri, Dictionary<string, string>? headerParameters = null, Dictionary<string, string>? queryParameters = null, string? json = null)
    {
        return await ExecuteAsync<T>(Method.Get, apiUrl, requestUri, headerParameters, queryParameters, json);
    }

    private static async Task<RestResponse<T>> ExecuteAsync<T>(Method method, string apiUrl, string requestUri, Dictionary<string, string>? headerParameters = null, Dictionary<string, string>? queryParameters = null, string? json = null)
    {
        using var client = new RestClient(apiUrl);

        var request = new RestRequest(requestUri, method);

        if (headerParameters != null)
            foreach (var parameter in headerParameters)
                if (!string.IsNullOrWhiteSpace(parameter.Value))
                    request.AddHeader(parameter.Key, parameter.Value);

        if (queryParameters != null)
            foreach (var parameter in queryParameters)
                if (!string.IsNullOrWhiteSpace(parameter.Value))
                    request.AddQueryParameter(parameter.Key, parameter.Value);

        if (!string.IsNullOrWhiteSpace(json))
            request.AddJsonBody(json);

        return await client.ExecuteAsync<T>(request);
    }
}