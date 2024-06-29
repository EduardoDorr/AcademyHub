using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using AcademyHub.Common.Persistence.UnitOfWork;
using AcademyHub.Common.Persistence.DbConnectionFactories;

using AcademyHub.Common.Auth;
using AcademyHub.Domain.Users;
using AcademyHub.Domain.Courses;
using AcademyHub.Domain.Lessons;
using AcademyHub.Domain.Enrollments;
using AcademyHub.Domain.CourseModules;
using AcademyHub.Domain.Subscriptions;
using AcademyHub.Domain.LearningTracks;
using AcademyHub.Domain.EnrollmentPayments;

using AcademyHub.Application.Abstractions.PaymentGateway;

using AcademyHub.Infrastructure.Auth;
using AcademyHub.Infrastructure.Interceptors;
using AcademyHub.Infrastructure.BackgroundJobs;
using AcademyHub.Infrastructure.Persistence.Contexts;
using AcademyHub.Infrastructure.Persistence.UnitOfWork;
using AcademyHub.Infrastructure.Persistence.Repositories;
using AcademyHub.Infrastructure.Integrations.Asaas.Apis;
using AcademyHub.Domain.LessonFinisheds;

namespace AcademyHub.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration)
                .AddInterceptors()
                .AddRepositories()
                .AddUnitOfWork()
                .AddAuthentication()
                .AddBackgroundJobs()
                .AddIntegrations();

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString();

        services.AddDbContext<AcademyHubDbContext>((sp, opts) =>
        {
            opts.UseSqlServer(connectionString)
                .AddInterceptors(
                    sp.GetRequiredService<PublishDomainEventsToOutBoxMessagesInterceptor>());
        });

        return services;
    }

    private static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        services.AddSingleton<PublishDomainEventsToOutBoxMessagesInterceptor>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();
        services.AddTransient<ILearningTrackRepository, LearningTrackRepository>();
        services.AddTransient<ICourseRepository, CourseRepository>();
        services.AddTransient<ICourseModuleRepository, CourseModuleRepository>();
        services.AddTransient<ILessonRepository, LessonRepository>();
        services.AddTransient<ILessonFinishedRepository, LessonFinishedRepository>();
        services.AddTransient<IEnrollmentRepository, EnrollmentRepository>();
        services.AddTransient<IEnrollmentPaymentRepository, EnrollmentPaymentRepository>();

        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();

        return services;
    }

    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddHostedService<ProcessOutboxMessagesJob>();

        return services;
    }

    private static IServiceCollection AddIntegrations(this IServiceCollection services)
    {
        services.AddTransient<IPaymentGateway, AsaasPaymentGatewayApi>();

        return services;
    }
}