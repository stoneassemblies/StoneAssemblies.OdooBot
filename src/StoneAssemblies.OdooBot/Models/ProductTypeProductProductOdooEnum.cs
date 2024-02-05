using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Tests;

/// <summary>
/// Help: A storable product is a product for which you manage stock. The Inventory app has to be installed. <br />
/// A consumable product is a product for which stock is not managed. <br />
/// A service is a non-material product you provide.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ProductTypeProductProductOdooEnum
{
    [EnumMember(Value = "consu")] Consumable = 1,

    [EnumMember(Value = "service")] Service = 2,

    [EnumMember(Value = "product")] StorableProduct = 3
}