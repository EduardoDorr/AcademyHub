using Microsoft.Extensions.Configuration;

using AcademyHub.Common.Persistence.DbConnectionFactories;

namespace AcademyHub.Common.Persistence.DbConnectionFactories;

public static class DbConnectionFactory
{
    public static string GetConnectionString(this IConfiguration configuration)
    {
        if (Environment.GetEnvironmentVariable("DOCKER_ENVIROMENT") == "DockerDevelopment")
            return configuration.GetConnectionString("ContainerConnection")!;

        return configuration.GetConnectionString("LocalConnection")!;
    }
}