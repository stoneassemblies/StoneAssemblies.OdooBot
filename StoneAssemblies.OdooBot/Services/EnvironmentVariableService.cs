using Microsoft.Extensions.Logging.Abstractions;
using StoneAssemblies.OdooBot.Services.Interfaces;

namespace StoneAssemblies.OdooBot.Services
{
    public class EnvironmentVariableService(ILogger<EnvironmentVariableService> logger) : IEnvironmentVariableService
    {
        public string? GetValue(string name)
        {
            var value = Environment.GetEnvironmentVariable(name);

            // Note: never log the actual values for security reasons
            logger.LogDebug("Getting value for environment variable '{Name}', returned any value: '{HasValue}'", name, !string.IsNullOrWhiteSpace(value));

            return value;
        }

        public string? GetValue(string name, EnvironmentVariableTarget target)
        {
            var value = Environment.GetEnvironmentVariable(name, target);

            // Note: never log the actual values for security reasons
            logger.LogDebug("Getting value for environment variable '{Name}' and target '{Target}', returned any value: '{HasValue}'", name, target, !string.IsNullOrWhiteSpace(value));

            return value;
        }
    }
}