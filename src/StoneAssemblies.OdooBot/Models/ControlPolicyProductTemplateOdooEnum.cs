using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Tests;

/// <summary>
/// Help: On ordered quantities: Control bills based on ordered quantities. <br />
/// On received quantities: Control bills based on received quantities.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ControlPolicyProductTemplateOdooEnum
{
    [EnumMember(Value = "purchase")] OnOrderedQuantities = 1,

    [EnumMember(Value = "receive")] OnReceivedQuantities = 2
}