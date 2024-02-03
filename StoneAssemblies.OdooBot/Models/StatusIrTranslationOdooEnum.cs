using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Models;

/// <summary>
/// Help: Automatically set to let administators find new terms that might need to be translated
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum StatusIrTranslationOdooEnum
{
    [EnumMember(Value = "to_translate")] ToTranslate = 1,

    [EnumMember(Value = "inprogress")] TranslationInProgress = 2,

    [EnumMember(Value = "translated")] Translated = 3
}