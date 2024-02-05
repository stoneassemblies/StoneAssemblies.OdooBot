namespace StoneAssemblies.OdooBot.Services.Interfaces
{
    public interface IEnvironmentVariableService
    {
        string? GetValue(string name);

        string? GetValue(string name, EnvironmentVariableTarget target);
    }
}