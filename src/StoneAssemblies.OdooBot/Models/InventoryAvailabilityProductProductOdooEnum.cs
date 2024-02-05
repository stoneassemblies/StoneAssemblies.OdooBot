using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Tests;

/// <summary>
/// Help: Adds an inventory availability status on the web product page.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum InventoryAvailabilityProductProductOdooEnum
{
    [EnumMember(Value = "never")] SellRegardlessOfInventory = 1,

    [EnumMember(Value = "always")] ShowInventoryOnWebsiteAndPreventSalesIfNotEnoughStock = 2,

    [EnumMember(Value = "threshold")] ShowInventoryBelowAThresholdAndPreventSalesIfNotEnoughStock = 3,

    [EnumMember(Value = "custom")] ShowProductSpecificNotifications = 4
}