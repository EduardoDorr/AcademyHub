namespace AcademyHub.Common.Options;

public class WebMailApiOptions
{
    public required string ApiName { get; set; }
    public required string BaseUrl { get; set; }
    public required string EmailEndpoint { get; set; }
}