using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Models;

/// <summary>
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum TypeIrTranslationOdooEnum
{
    [EnumMember(Value = "model")] ModelField = 1,

    [EnumMember(Value = "model_terms")] StructuredModelField = 2,

    [EnumMember(Value = "code")] Code = 3
}