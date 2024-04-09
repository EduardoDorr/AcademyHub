using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using FluentValidation;
using FluentValidation.AspNetCore;

namespace AcademyHub.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator()
                .AddValidator()
                .AddServices()
                .AddBackgroundJobs();

        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }

    private static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);
        services.AddFluentValidationAutoValidation();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        //services.AddTransient<IReportService, ReportService>();
        //services.AddTransient<IInterestTransactionService, InterestTransactionService>();

        return services;
    }

    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        //services.AddHostedService<ProcessInterestTransactionsJob>();

        return services;
    }
}