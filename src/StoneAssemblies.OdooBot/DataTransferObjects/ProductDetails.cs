using StoneAssemblies.OdooBot.Entities;

namespace StoneAssemblies.OdooBot.DataTransferObjects;

public class ProductDetails
{
    public Guid Id { get; set; }

    public long ExternalId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double InStockQuantity { get; set; }

    public double StandardPrice { get; set; }

    public string QuantityUnit { get; set; }

    public IEnumerable<Image> FeatureImages { get; set; }
}