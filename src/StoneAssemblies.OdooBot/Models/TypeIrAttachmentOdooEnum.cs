using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Models;

/// <summary>
/// Help: You can either upload a file from your computer or copy/paste an internet link to your file.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum TypeIrAttachmentOdooEnum
{
    [EnumMember(Value = "url")] URL = 1,

    [EnumMember(Value = "binary")] File = 2
}