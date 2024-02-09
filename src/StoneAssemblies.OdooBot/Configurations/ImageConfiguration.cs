using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoneAssemblies.OdooBot.Entities;

namespace StoneAssemblies.OdooBot.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder
            .HasOne(image => image.Product)
            .WithMany(product => product.Images)
            .HasForeignKey(product => product.ProductId);

        builder.Property(image => image.ExternalId).IsRequired(false);
        builder.HasIndex(image => new { image.Size, image.ExternalId }).IsUnique().HasFilter("ExternalId IS NOT NULL");
    }
}