namespace StoneAssemblies.OdooBot.Models;

using Newtonsoft.Json;

using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;

[OdooTableName("product.pricelist")]
[JsonConverter(typeof(OdooModelConverter))]
public class ProductPricelistOdooModel : IOdooModel
{

    /// <summary>
    /// name - char  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// active - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: If unchecked, it will allow you to hide the pricelist without removing it. <br />
    /// </summary>
    [JsonProperty("active")]
    public bool? Active { get; set; }

    /// <summary>
    /// item_ids - one2many - product.pricelist.item (pricelist_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("item_ids")]
    public long[] ItemIds { get; set; }

    /// <summary>
    /// currency_id - many2one - res.currency <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("currency_id")]
    public long CurrencyId { get; set; }

    /// <summary>
    /// company_id - many2one - res.company <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("company_id")]
    public long? CompanyId { get; set; }

    /// <summary>
    /// sequence - integer  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("sequence")]
    public int? Sequence { get; set; }

    /// <summary>
    /// country_group_ids - many2many - res.country.group <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("country_group_ids")]
    public long[] CountryGroupIds { get; set; }

    /// <summary>
    /// discount_policy - selection  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("discount_policy")]
    public DiscountPolicyProductPricelistOdooEnum DiscountPolicy { get; set; }

    /// <summary>
    /// website_id - many2one - website <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("website_id")]
    public long? WebsiteId { get; set; }

    /// <summary>
    /// code - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    /// selectable - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Allow the end user to choose this price list <br />
    /// </summary>
    [JsonProperty("selectable")]
    public bool? Selectable { get; set; }

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
    /// create_uid - many2one - res.users <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("create_uid")]
    public long? CreateUid { get; set; }

    /// <summary>
    /// create_date - datetime  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("create_date")]
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// write_uid - many2one - res.users <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("write_uid")]
    public long? WriteUid { get; set; }

    /// <summary>
    /// write_date - datetime  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("write_date")]
    public DateTime? WriteDate { get; set; }

    /// <summary>
    /// __last_update - datetime  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("__last_update")]
    public DateTime? LastUpdate { get; set; }
}