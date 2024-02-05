using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Models;

/// <summary>
/// Help: Status based on activities <br />
/// Overdue: Due date is already passed <br />
/// Today: Activity date is today <br />
/// Planned: Future activities.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ActivityStateProductProductOdooEnum
{
    [EnumMember(Value = "overdue")] Overdue = 1,

    [EnumMember(Value = "today")] Today = 2,

    [EnumMember(Value = "planned")] Planned = 3
}