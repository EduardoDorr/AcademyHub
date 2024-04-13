using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace AcademyHub.Common.Swagger;

public static class SwaggerExtensions
{
    public static void UseCommonSwaggerDoc(this SwaggerGenOptions options, string name, string version)
    {
        options.SwaggerDoc(version, new OpenApiInfo
        {
            Title = name,
            Version = version,
            Contact = new OpenApiContact
            {
                Name = "Eduardo Dörr",
                Email = "edudorr@hotmail.com",
                Url = new Uri("https://github.com/EduardoDorr")
            }
        });
    }

    public static void UseCommonAuthorizationBearer(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using bearer scheme."
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    }
}