namespace StoneAssemblies.OdooBot.Models;

using Newtonsoft.Json;

using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;

using StoneAssemblies.OdooBot.Tests;

[OdooTableName("product.category")]
[JsonConverter(typeof(OdooModelConverter))]
public class ProductCategoryOdooModel : IOdooModel
{
    /// <summary>
    /// name - char  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// complete_name - char  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("complete_name")]
    public string CompleteName { get; set; }

    /// <summary>
    /// parent_id - many2one - product.category <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("parent_id")]
    public long? ParentId { get; set; }

    /// <summary>
    /// parent_path - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("parent_path")]
    public string ParentPath { get; set; }

    /// <summary>
    /// child_id - one2many - product.category (parent_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("child_id")]
    public long[] ChildId { get; set; }

    /// <summary>
    /// product_count - integer  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: The number of products under this category (Does not consider the children categories) <br />
    /// </summary>
    [JsonProperty("product_count")]
    public int? ProductCount { get; set; }

    /// <summary>
    /// property_account_income_categ_id - many2one - account.account <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: This account will be used when validating a customer invoice. <br />
    /// </summary>
    [JsonProperty("property_account_income_categ_id")]
    public long? PropertyAccountIncomeCategId { get; set; }

    /// <summary>
    /// property_account_expense_categ_id - many2one - account.account <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: The expense is accounted for when a vendor bill is validated, except in anglo-saxon accounting with perpetual inventory valuation in which case the expense (Cost of Goods Sold account) is recognized at the customer invoice validation. <br />
    /// </summary>
    [JsonProperty("property_account_expense_categ_id")]
    public long? PropertyAccountExpenseCategId { get; set; }

    /// <summary>
    /// route_ids - many2many - stock.location.route <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("route_ids")]
    public long[] RouteIds { get; set; }

    /// <summary>
    /// removal_strategy_id - many2one - product.removal <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Set a specific removal strategy that will be used regardless of the source location for this product category <br />
    /// </summary>
    [JsonProperty("removal_strategy_id")]
    public long? RemovalStrategyId { get; set; }

    /// <summary>
    /// total_route_ids - many2many - stock.location.route <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("total_route_ids")]
    public long[] TotalRouteIds { get; set; }

    /// <summary>
    /// putaway_rule_ids - one2many - stock.putaway.rule (category_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("putaway_rule_ids")]
    public long[] PutawayRuleIds { get; set; }

    /// <summary>
    /// property_account_creditor_price_difference_categ - many2one - account.account <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: This account will be used to value price difference between purchase price and accounting cost. <br />
    /// </summary>
    [JsonProperty("property_account_creditor_price_difference_categ")]
    public long? PropertyAccountCreditorPriceDifferenceCateg { get; set; }

    /// <summary>
    /// property_valuation - selection  <br />
    /// Required: True, Readonly: False, Store: False, Sortable: False <br />
    /// Help: Manual: The accounting entries to value the inventory are not posted automatically.;         Automated: An accounting entry is automatically created to value the inventory when a product enters or leaves the company.;          <br />
    /// </summary>
    [JsonProperty("property_valuation")]
    public InventoryValuationProductCategoryOdooEnum PropertyValuation { get; set; }

    /// <summary>
    /// property_cost_method - selection  <br />
    /// Required: True, Readonly: False, Store: False, Sortable: False <br />
    /// Help: Standard Price: The products are valued at their standard cost defined on the product.;         Average Cost (AVCO): The products are valued at weighted average cost.;         First In First Out (FIFO): The products are valued supposing those that enter the company first will also leave it first.;          <br />
    /// </summary>
    [JsonProperty("property_cost_method")]
    public CostingMethodProductCategoryOdooEnum PropertyCostMethod { get; set; }

    /// <summary>
    /// property_stock_journal - many2one - account.journal <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: When doing automated inventory valuation, this is the Accounting Journal in which entries will be automatically posted when stock moves are processed. <br />
    /// </summary>
    [JsonProperty("property_stock_journal")]
    public long? PropertyStockJournal { get; set; }

    /// <summary>
    /// property_stock_account_input_categ_id - many2one - account.account <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: Counterpart journal items for all incoming stock moves will be posted in this account, unless there is a specific valuation account;                 set on the source location. This is the default value for all products in this category. It can also directly be set on each product. <br />
    /// </summary>
    [JsonProperty("property_stock_account_input_categ_id")]
    public long? PropertyStockAccountInputCategId { get; set; }

    /// <summary>
    /// property_stock_account_output_categ_id - many2one - account.account <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: When doing automated inventory valuation, counterpart journal items for all outgoing stock moves will be posted in this account,;                 unless there is a specific valuation account set on the destination location. This is the default value for all products in this category.;                 It can also directly be set on each product. <br />
    /// </summary>
    [JsonProperty("property_stock_account_output_categ_id")]
    public long? PropertyStockAccountOutputCategId { get; set; }

    /// <summary>
    /// property_stock_valuation_account_id - many2one - account.account <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: When automated inventory valuation is enabled on a product, this account will hold the current value of the products. <br />
    /// </summary>
    [JsonProperty("property_stock_valuation_account_id")]
    public long? PropertyStockValuationAccountId { get; set; }

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