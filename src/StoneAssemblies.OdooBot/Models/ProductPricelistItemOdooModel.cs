namespace StoneAssemblies.OdooBot.Models;

using Newtonsoft.Json;

using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;

[OdooTableName("product.pricelist.item")]
[JsonConverter(typeof(OdooModelConverter))]
public class ProductPricelistItemOdooModel : IOdooModel
{

    /// <summary>
    /// product_tmpl_id - many2one - product.template <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Specify a template if this rule only applies to one product template. Keep empty otherwise. <br />
    /// </summary>
    [JsonProperty("product_tmpl_id")]
    public long? ProductTmplId { get; set; }

    /// <summary>
    /// product_id - many2one - product.product <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Specify a product if this rule only applies to one product. Keep empty otherwise. <br />
    /// </summary>
    [JsonProperty("product_id")]
    public long? ProductId { get; set; }

    /// <summary>
    /// categ_id - many2one - product.category <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Specify a product category if this rule only applies to products belonging to this category or its children categories. Keep empty otherwise. <br />
    /// </summary>
    [JsonProperty("categ_id")]
    public long? CategId { get; set; }

    /// <summary>
    /// min_quantity - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: For the rule to apply, bought/sold quantity must be greater than or equal to the minimum quantity specified in this field.; Expressed in the default unit of measure of the product. <br />
    /// </summary>
    [JsonProperty("min_quantity")]
    public double? MinQuantity { get; set; }

    /// <summary>
    /// applied_on - selection  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Pricelist Item applicable on selected option <br />
    /// </summary>
    [JsonProperty("applied_on")]
    public ApplyOnProductPricelistItemOdooEnum AppliedOn { get; set; }

    /// <summary>
    /// base - selection  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Base price for computation.; Sales Price: The base price will be the Sales Price.; Cost Price : The base price will be the cost price.; Other Pricelist : Computation of the base price based on another Pricelist. <br />
    /// </summary>
    [JsonProperty("base")]
    public BasedOnProductPricelistItemOdooEnum Base { get; set; }

    /// <summary>
    /// base_pricelist_id - many2one - product.pricelist <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("base_pricelist_id")]
    public long? BasePricelistId { get; set; }

    /// <summary>
    /// pricelist_id - many2one - product.pricelist <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("pricelist_id")]
    public long PricelistId { get; set; }

    /// <summary>
    /// price_surcharge - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Specify the fixed amount to add or subtract(if negative) to the amount calculated with the discount. <br />
    /// </summary>
    [JsonProperty("price_surcharge")]
    public double? PriceSurcharge { get; set; }

    /// <summary>
    /// price_discount - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("price_discount")]
    public double? PriceDiscount { get; set; }

    /// <summary>
    /// price_round - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Sets the price so that it is a multiple of this value.; Rounding is applied after the discount and before the surcharge.; To have prices that end in 9.99, set rounding 10, surcharge -0.01 <br />
    /// </summary>
    [JsonProperty("price_round")]
    public double? PriceRound { get; set; }

    /// <summary>
    /// price_min_margin - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Specify the minimum amount of margin over the base price. <br />
    /// </summary>
    [JsonProperty("price_min_margin")]
    public double? PriceMinMargin { get; set; }

    /// <summary>
    /// price_max_margin - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Specify the maximum amount of margin over the base price. <br />
    /// </summary>
    [JsonProperty("price_max_margin")]
    public double? PriceMaxMargin { get; set; }

    /// <summary>
    /// company_id - many2one - res.company <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("company_id")]
    public long? CompanyId { get; set; }

    /// <summary>
    /// currency_id - many2one - res.currency <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("currency_id")]
    public long? CurrencyId { get; set; }

    /// <summary>
    /// active - boolean  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// Help: If unchecked, it will allow you to hide the pricelist without removing it. <br />
    /// </summary>
    [JsonProperty("active")]
    public bool? Active { get; set; }

    /// <summary>
    /// date_start - datetime  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Starting datetime for the pricelist item validation; The displayed value depends on the timezone set in your preferences. <br />
    /// </summary>
    [JsonProperty("date_start")]
    public DateTime? DateStart { get; set; }

    /// <summary>
    /// date_end - datetime  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Ending datetime for the pricelist item validation; The displayed value depends on the timezone set in your preferences. <br />
    /// </summary>
    [JsonProperty("date_end")]
    public DateTime? DateEnd { get; set; }

    /// <summary>
    /// compute_price - selection  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("compute_price")]
    public ComputePriceProductPricelistItemOdooEnum ComputePrice { get; set; }

    /// <summary>
    /// fixed_price - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("fixed_price")]
    public double? FixedPrice { get; set; }

    /// <summary>
    /// percent_price - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("percent_price")]
    public double? PercentPrice { get; set; }

    /// <summary>
    /// name - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Explicit rule name for this pricelist line. <br />
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// price - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Explicit rule name for this pricelist line. <br />
    /// </summary>
    [JsonProperty("price")]
    public string Price { get; set; }

    /// <summary>
    /// use_new_rate - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("use_new_rate")]
    public bool? UseNewRate { get; set; }

    /// <summary>
    /// dr_offer_msg - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("dr_offer_msg")]
    public string DrOfferMsg { get; set; }

    /// <summary>
    /// dr_offer_finish_msg - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("dr_offer_finish_msg")]
    public string DrOfferFinishMsg { get; set; }

    /// <summary>
    /// offer_msg - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: To set the message in the product offer timer. <br />
    /// </summary>
    [JsonProperty("offer_msg")]
    public string OfferMsg { get; set; }

    /// <summary>
    /// is_display_timer - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: It shows the product timer on product page. <br />
    /// </summary>
    [JsonProperty("is_display_timer")]
    public bool? IsDisplayTimer { get; set; }

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