using AcademyHub.Common.Results;
using AcademyHub.Common.IntegrationsEvents;

namespace AcademyHub.Notifications.EmailApi;

public interface IWebMailApi
{
    Task<Result> SendEmail(SendEmailEvent email);
}