using System.ComponentModel.DataAnnotations;

namespace StoneAssemblies.OdooBot.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    public long ExternalId { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public List<Image> Images { get; set; }

    public double InStockQuantity { get; set; }

    public double IncomingQuantity { get; set; }

    public string QuantityUnit { get; set; }

    public double StandardPrice { get; set; }

    public double Price { get; set; }
}