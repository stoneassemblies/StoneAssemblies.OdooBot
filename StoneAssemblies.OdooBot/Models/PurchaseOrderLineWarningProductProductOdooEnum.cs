using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Tests;

/// <summary>
/// Help: Selecting the "Warning" option will notify user with the message, Selecting "Blocking Message" will throw an exception with the message and block the flow. The Message has to be written in the next field.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum PurchaseOrderLineWarningProductProductOdooEnum
{
    [EnumMember(Value = "no-message")] NoMessage = 1,

    [EnumMember(Value = "warning")] Warning = 2,

    [EnumMember(Value = "block")] BlockingMessage = 3
}