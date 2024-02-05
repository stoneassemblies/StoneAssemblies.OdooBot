using Microsoft.EntityFrameworkCore;
using StoneAssemblies.OdooBot.Entities;

namespace StoneAssemblies.OdooBot.Services;

public class ApplicationDbContext(ILogger<ApplicationDbContext> logger) : DbContext
{
    public DbSet<Category>? Categories { get; set; }

    public DbSet<Product>? Products { get; set; }
    
    public DbSet<Image>? Images { get; set; }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "odoo-local-cache.db")}");
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}