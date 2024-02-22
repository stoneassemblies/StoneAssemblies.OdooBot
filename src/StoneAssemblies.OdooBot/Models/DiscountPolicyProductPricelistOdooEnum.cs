namespace StoneAssemblies.OdooBot.Models;

using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/// <summary>
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum DiscountPolicyProductPricelistOdooEnum
{
    [EnumMember(Value = "with_discount")]
    DiscountIncludedInThePrice = 1,

    [EnumMember(Value = "without_discount")]
    ShowPublicPriceDiscountToTheCustomer = 2,
}