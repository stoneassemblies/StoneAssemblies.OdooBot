using System.ComponentModel.DataAnnotations;

namespace StoneAssemblies.OdooBot.Entities;

public class Image
{
    [Key]
    public Guid Id { get; set; }

    public long? ExternalId { get; set; }

    public Guid ProductId { get; set; }

    public Product Product { get; set; }

    public ImageSize Size { get; set; }

    public byte[] Content { get; set; }

    public bool IsFeatured { get; set; }

    public DateTime? LastUpdate { get; set; }
}