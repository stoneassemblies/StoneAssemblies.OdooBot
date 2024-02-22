namespace StoneAssemblies.OdooBot.Models;

using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/// <summary>
/// Help: Base price for computation. <br />
/// Sales Price: The base price will be the Sales Price. <br />
/// Cost Price : The base price will be the cost price. <br />
/// Other Pricelist : Computation of the base price based on another Pricelist.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum BasedOnProductPricelistItemOdooEnum
{
    [EnumMember(Value = "list_price")]
    SalesPrice = 1,

    [EnumMember(Value = "standard_price")]
    Cost = 2,

    [EnumMember(Value = "pricelist")]
    OtherPricelist = 3,
}