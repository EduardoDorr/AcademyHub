using AcademyHub.Common.Results.Errors;

namespace AcademyHub.Infrastructure.Integrations.Asaas.Dtos.Errors;

internal sealed record ErrorDtoResponse(Error[] Errors);

internal sealed record Error(string Code, string Description);

internal static class ErrorDtoResponseExtension
{
    public static List<Common.Results.Errors.Error> ToError(this ErrorDtoResponse? response)
    {
        return response is not null
             ? response.Errors.Select(r => new Common.Results.Errors.Error(r.Code, r.Description, ErrorType.Validation)).ToList()
             : [];
    }
}