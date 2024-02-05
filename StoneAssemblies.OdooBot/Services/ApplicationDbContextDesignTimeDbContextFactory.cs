using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging.Abstractions;

namespace StoneAssemblies.OdooBot.Services;

/// <summary>
/// The application db context design time db context factory.
/// </summary>
public class ApplicationDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    /// <summary>
    /// The create db context.
    /// </summary>
    /// <param name="args">
    /// The args.
    /// </param>
    /// <returns>
    /// The <see cref="ApplicationDbContext"/>.
    /// </returns>
    /// <exception cref="Exception">
    /// The expected 'environment' argument is not specified.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// The specified environment {environment} is not supported.
    /// </exception>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        return new ApplicationDbContext(NullLogger<ApplicationDbContext>.Instance);
    }
}