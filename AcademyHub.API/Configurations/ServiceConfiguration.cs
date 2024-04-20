using System.Text;
using System.Text.Json.Serialization;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Serilog;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

using AcademyHub.Common;
using AcademyHub.Common.Swagger;
using AcademyHub.Common.Options;

using AcademyHub.API.Middlewares;
using AcademyHub.Application;
using AcademyHub.Infrastructure;
using AcademyHub.Notifications;

namespace AcademyHub.API.Configurations;

public static class ServiceConfiguration
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add Serilog as the log provider.
        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog();
        });

        builder.Services.ConfigureOptions(builder.Configuration);

        // Add modules
        builder.Services.AddCommon();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddNotificationModule();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(s =>
        {
            s.UseCommonSwaggerDoc("AcademyHub.API", "v1");

            s.UseCommonAuthorizationBearer();

            s.AddEnumsWithValuesFixFilters();
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    ValidAudience = builder.Configuration["Authentication:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Key"]))
                };
            });

        return builder;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.UseStaticFiles();

        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(builder.Configuration)
           .CreateLogger();
    }

    private static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AsaasApiOptions>(options => configuration.GetSection(OptionsConstants.AsaasApiSection).Bind(options));
        services.Configure<WebMailApiOptions>(options => configuration.GetSection(OptionsConstants.WebMailApiSection).Bind(options));
        services.Configure<AuthenticationOptions>(options => configuration.GetSection(OptionsConstants.AuthenticationSection).Bind(options));
        services.Configure<RabbitMqConfigurationOptions>(options => configuration.GetSection(OptionsConstants.RabbitMQConfigurationSection).Bind(options));
    }
}