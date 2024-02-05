using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Tests;

/// <summary>
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum LanguageIrTranslationOdooEnum
{
    [EnumMember(Value = "en_US")] EnglishUS = 1,

    [EnumMember(Value = "es_ES")] SpanishEspaOl = 2
}