using Newtonsoft.Json;
using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;
using StoneAssemblies.OdooBot.Models;

namespace StoneAssemblies.OdooBot.Tests;

[OdooTableName("ir.translation")]
[JsonConverter(typeof(OdooModelConverter))]
public class IrTranslationOdooModel : IOdooModel
{
    /// <summary>
    /// name - char  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// res_id - integer  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("res_id")]
    public int? ResId { get; set; }

    /// <summary>
    /// lang - selection  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("lang")]
    public LanguageIrTranslationOdooEnum? Lang { get; set; }

    /// <summary>
    /// type - selection  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("type")]
    public TypeIrTranslationOdooEnum? Type { get; set; }

    /// <summary>
    /// src - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("src")]
    public string Src { get; set; }

    /// <summary>
    /// value - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("value")]
    public string Value { get; set; }

    /// <summary>
    /// module - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Module this term belongs to <br />
    /// </summary>
    [JsonProperty("module")]
    public string Module { get; set; }

    /// <summary>
    /// state - selection  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Automatically set to let administators find new terms that might need to be translated <br />
    /// </summary>
    [JsonProperty("state")]
    public StatusIrTranslationOdooEnum? State { get; set; }

    /// <summary>
    /// comments - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("comments")]
    public string Comments { get; set; }

    /// <summary>
    /// id - integer  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// display_name - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("display_name")]
    public string DisplayName { get; set; }

    /// <summary>
    /// __last_update - datetime  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("__last_update")]
    public DateTime? LastUpdate { get; set; }
}