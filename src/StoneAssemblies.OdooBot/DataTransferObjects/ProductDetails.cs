using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using StoneAssemblies.OdooBot.Entities;

namespace StoneAssemblies.OdooBot.DataTransferObjects;

public class ProductDetails
{
    public Guid Id { get; set; }
    public long ExternalId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<Image> FeatureImages { get; set; }
}