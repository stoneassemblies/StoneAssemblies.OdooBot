using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Models;

/// <summary>
/// Help: Type of the exception activity on record.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ActivityExceptionDecorationProductTemplateOdooEnum
{
    [EnumMember(Value = "warning")] Alert = 1,

    [EnumMember(Value = "danger")] Error = 2
}