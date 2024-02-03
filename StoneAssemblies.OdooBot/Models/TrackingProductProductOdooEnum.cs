using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Models;

/// <summary>
/// Help: Ensure the traceability of a storable product in your warehouse.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum TrackingProductProductOdooEnum
{
    [EnumMember(Value = "serial")] ByUniqueSerialNumber = 1,

    [EnumMember(Value = "lot")] ByLots = 2,

    [EnumMember(Value = "none")] NoTracking = 3
}