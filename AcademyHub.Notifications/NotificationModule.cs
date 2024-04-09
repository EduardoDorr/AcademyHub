using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

using AcademyHub.Common.Options;

using AcademyHub.Notifications.EmailApi;
using AcademyHub.Notifications.Consumers;

namespace AcademyHub.Notifications;

public static class NotificationModule
{
    public static IServiceCollection AddNotificationModule(this IServiceCollection services)
    {
        services.AddServices()
                .AddConsumers()
                .AddHttpClients();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IWebMailApi, WebMailApi>();

        return services;
    }

    private static IServiceCollection AddConsumers(this IServiceCollection services)
    {
        services.AddHostedService<SendEmailEventConsumerService>();

        return services;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        var webMailApiOptions = services.BuildServiceProvider().GetRequiredService<IOptions<WebMailApiOptions>>().Value;

        services.AddHttpClient(webMailApiOptions.ApiName, client =>
        {
            client.BaseAddress = new Uri(webMailApiOptions.BaseUrl);
        });

        return services;
    }
}