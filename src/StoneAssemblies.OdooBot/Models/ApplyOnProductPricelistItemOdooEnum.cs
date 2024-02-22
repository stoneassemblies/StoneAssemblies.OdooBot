namespace StoneAssemblies.OdooBot.Models;

using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/// <summary>
/// Help: Pricelist Item applicable on selected option
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ApplyOnProductPricelistItemOdooEnum
{
    [EnumMember(Value = "3_global")]
    AllProducts = 1,

    [EnumMember(Value = "2_product_category")]
    ProductCategory = 2,

    [EnumMember(Value = "1_product")]
    Product = 3,

    [EnumMember(Value = "0_product_variant")]
    ProductVariant = 4,
}