using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Tests;

/// <summary>
/// Help: Manual: The accounting entries to value the inventory are not posted automatically. <br />
///         Automated: An accounting entry is automatically created to value the inventory when a product enters or leaves the company. <br />
///         
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum InventoryValuationProductTemplateOdooEnum
{
    [EnumMember(Value = "manual_periodic")]
    Manual = 1,

    [EnumMember(Value = "real_time")] Automated = 2
}