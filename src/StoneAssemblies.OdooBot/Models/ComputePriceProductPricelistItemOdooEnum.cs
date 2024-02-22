namespace StoneAssemblies.OdooBot.Models;

using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/// <summary>
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ComputePriceProductPricelistItemOdooEnum
{
    [EnumMember(Value = "fixed")]
    FixedPrice = 1,

    [EnumMember(Value = "percentage")]
    PercentageDiscount = 2,

    [EnumMember(Value = "formula")]
    Formula = 3,
}