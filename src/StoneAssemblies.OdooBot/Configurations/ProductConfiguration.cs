using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoneAssemblies.OdooBot.Entities;

namespace StoneAssemblies.OdooBot.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasOne(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => product.CategoryId);

        builder.Property(p => p.AggregateQuantity)
            .HasComputedColumnSql("[InStockQuantity] + [IncomingQuantity]");

        builder
            .HasIndex(product => product.ExternalId).IsUnique();
    }
}