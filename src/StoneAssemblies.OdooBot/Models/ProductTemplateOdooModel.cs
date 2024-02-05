using Newtonsoft.Json;
using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;
using StoneAssemblies.OdooBot.Models;

namespace StoneAssemblies.OdooBot.Tests;

[OdooTableName("product.template")]
[JsonConverter(typeof(OdooModelConverter))]
public class ProductTemplateOdooModel : IOdooModel
{
    /// <summary>
    /// name - char  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// sequence - integer  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Gives the sequence order when displaying a product list <br />
    /// </summary>
    [JsonProperty("sequence")]
    public int? Sequence { get; set; }

    /// <summary>
    /// description - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    /// description_purchase - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("description_purchase")]
    public string DescriptionPurchase { get; set; }

    /// <summary>
    /// description_sale - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: A description of the Product that you want to communicate to your customers. This description will be copied to every Sales Order, Delivery Order and Customer Invoice/Credit Note <br />
    /// </summary>
    [JsonProperty("description_sale")]
    public string DescriptionSale { get; set; }

    /// <summary>
    /// categ_id - many2one - product.category <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Select category for the current product <br />
    /// </summary>
    [JsonProperty("categ_id")]
    public long CategId { get; set; }

    /// <summary>
    /// currency_id - many2one - res.currency <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("currency_id")]
    public long? CurrencyId { get; set; }

    /// <summary>
    /// cost_currency_id - many2one - res.currency <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("cost_currency_id")]
    public long? CostCurrencyId { get; set; }

    /// <summary>
    /// price - float  <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("price")]
    public double? Price { get; set; }

    /// <summary>
    /// list_price - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Price at which the product is sold to customers. <br />
    /// </summary>
    [JsonProperty("list_price")]
    public double? ListPrice { get; set; }

    /// <summary>
    /// lst_price - float  <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: Price at which the product is sold to customers. <br />
    /// </summary>
    [JsonProperty("lst_price")]
    public double? LstPrice { get; set; }

    /// <summary>
    /// standard_price - float  <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: In Standard Price & AVCO: value of the product (automatically computed in AVCO).;         In FIFO: value of the next unit that will leave the stock (automatically computed).;         Used to value the product when the purchase cost is not known (e.g. inventory adjustment).;         Used to compute margins on sale orders. <br />
    /// </summary>
    [JsonProperty("standard_price")]
    public double? StandardPrice { get; set; }

    /// <summary>
    /// volume - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("volume")]
    public double? Volume { get; set; }

    /// <summary>
    /// volume_uom_name - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("volume_uom_name")]
    public string VolumeUomName { get; set; }

    /// <summary>
    /// weight - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("weight")]
    public double? Weight { get; set; }

    /// <summary>
    /// weight_uom_name - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("weight_uom_name")]
    public string WeightUomName { get; set; }

    /// <summary>
    /// sale_ok - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("sale_ok")]
    public bool? SaleOk { get; set; }

    /// <summary>
    /// purchase_ok - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("purchase_ok")]
    public bool? PurchaseOk { get; set; }

    /// <summary>
    /// pricelist_id - many2one - product.pricelist <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: Technical field. Used for searching on pricelists, not stored in database. <br />
    /// </summary>
    [JsonProperty("pricelist_id")]
    public long? PricelistId { get; set; }

    /// <summary>
    /// uom_id - many2one - uom.uom <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Default unit of measure used for all stock operations. <br />
    /// </summary>
    [JsonProperty("uom_id")]
    public long UomId { get; set; }

    /// <summary>
    /// uom_name - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("uom_name")]
    public string UomName { get; set; }

    /// <summary>
    /// uom_po_id - many2one - uom.uom <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Default unit of measure used for purchase orders. It must be in the same category as the default unit of measure. <br />
    /// </summary>
    [JsonProperty("uom_po_id")]
    public long UomPoId { get; set; }

    /// <summary>
    /// company_id - many2one - res.company <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("company_id")]
    public long? CompanyId { get; set; }

    /// <summary>
    /// packaging_ids - one2many - product.packaging <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: Gives the different ways to package the same product. <br />
    /// </summary>
    [JsonProperty("packaging_ids")]
    public long[] PackagingIds { get; set; }

    /// <summary>
    /// seller_ids - one2many - product.supplierinfo (product_tmpl_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: Define vendor pricelists. <br />
    /// </summary>
    [JsonProperty("seller_ids")]
    public long[] SellerIds { get; set; }

    /// <summary>
    /// variant_seller_ids - one2many - product.supplierinfo (product_tmpl_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("variant_seller_ids")]
    public long[] VariantSellerIds { get; set; }

    /// <summary>
    /// active - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: If unchecked, it will allow you to hide the product without removing it. <br />
    /// </summary>
    [JsonProperty("active")]
    public bool? Active { get; set; }

    /// <summary>
    /// color - integer  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("color")]
    public int? Color { get; set; }

    /// <summary>
    /// is_product_variant - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("is_product_variant")]
    public bool? IsProductVariant { get; set; }

    /// <summary>
    /// attribute_line_ids - one2many - product.template.attribute.line (product_tmpl_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("attribute_line_ids")]
    public long[] AttributeLineIds { get; set; }

    /// <summary>
    /// valid_product_template_attribute_line_ids - many2many - product.template.attribute.line <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Technical compute <br />
    /// </summary>
    [JsonProperty("valid_product_template_attribute_line_ids")]
    public long[] ValidProductTemplateAttributeLineIds { get; set; }

    /// <summary>
    /// product_variant_ids - one2many - product.product (product_tmpl_id) <br />
    /// Required: True, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("product_variant_ids")]
    public long[] ProductVariantIds { get; set; }

    /// <summary>
    /// product_variant_id - many2one - product.product <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("product_variant_id")]
    public long? ProductVariantId { get; set; }

    /// <summary>
    /// product_variant_count - integer  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("product_variant_count")]
    public int? ProductVariantCount { get; set; }

    /// <summary>
    /// barcode - char  <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    /// default_code - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("default_code")]
    public string DefaultCode { get; set; }

    /// <summary>
    /// pricelist_item_count - integer  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("pricelist_item_count")]
    public int? PricelistItemCount { get; set; }

    /// <summary>
    /// can_image_1024_be_zoomed - boolean  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("can_image_1024_be_zoomed")]
    public bool? CanImage1024BeZoomed { get; set; }

    /// <summary>
    /// has_configurable_attributes - boolean  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("has_configurable_attributes")]
    public bool? HasConfigurableAttributes { get; set; }

    /// <summary>
    /// taxes_id - many2many - account.tax <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: Default taxes used when selling the product. <br />
    /// </summary>
    [JsonProperty("taxes_id")]
    public long[] TaxesId { get; set; }

    /// <summary>
    /// supplier_taxes_id - many2many - account.tax <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: Default taxes used when buying the product. <br />
    /// </summary>
    [JsonProperty("supplier_taxes_id")]
    public long[] SupplierTaxesId { get; set; }

    /// <summary>
    /// property_account_income_id - many2one - account.account <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: Keep this field empty to use the default value from the product category. <br />
    /// </summary>
    [JsonProperty("property_account_income_id")]
    public long? PropertyAccountIncomeId { get; set; }

    /// <summary>
    /// property_account_expense_id - many2one - account.account <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: Keep this field empty to use the default value from the product category. If anglo-saxon accounting with automated valuation method is configured, the expense account on the product category will be used. <br />
    /// </summary>
    [JsonProperty("property_account_expense_id")]
    public long? PropertyAccountExpenseId { get; set; }

    /// <summary>
    /// responsible_id - many2one - res.users <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: This user will be responsible of the next activities related to logistic operations for this product. <br />
    /// </summary>
    [JsonProperty("responsible_id")]
    public long? ResponsibleId { get; set; }

    /// <summary>
    /// type - selection  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// Help: A storable product is a product for which you manage stock. The Inventory app has to be installed.; A consumable product is a product for which stock is not managed.; A service is a non-material product you provide. <br />
    /// </summary>
    [JsonProperty("type")]
    public ProductTypeProductTemplateOdooEnum Type { get; set; }

    /// <summary>
    /// property_stock_production - many2one - stock.location <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: This stock location will be used, instead of the default one, as the source location for stock moves generated by manufacturing orders. <br />
    /// </summary>
    [JsonProperty("property_stock_production")]
    public long? PropertyStockProduction { get; set; }

    /// <summary>
    /// property_stock_inventory - many2one - stock.location <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: This stock location will be used, instead of the default one, as the source location for stock moves generated when you do an inventory. <br />
    /// </summary>
    [JsonProperty("property_stock_inventory")]
    public long? PropertyStockInventory { get; set; }

    /// <summary>
    /// sale_delay - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Delivery lead time, in days. It's the number of days, promised to the customer, between the confirmation of the sales order and the delivery. <br />
    /// </summary>
    [JsonProperty("sale_delay")]
    public double? SaleDelay { get; set; }

    /// <summary>
    /// tracking - selection  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Ensure the traceability of a storable product in your warehouse. <br />
    /// </summary>
    [JsonProperty("tracking")]
    public TrackingProductTemplateOdooEnum Tracking { get; set; }

    /// <summary>
    /// description_picking - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("description_picking")]
    public string DescriptionPicking { get; set; }

    /// <summary>
    /// description_pickingout - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("description_pickingout")]
    public string DescriptionPickingout { get; set; }

    /// <summary>
    /// description_pickingin - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("description_pickingin")]
    public string DescriptionPickingin { get; set; }

    /// <summary>
    /// qty_available - float  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("qty_available")]
    public double? QtyAvailable { get; set; }

    /// <summary>
    /// virtual_available - float  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("virtual_available")]
    public double? VirtualAvailable { get; set; }

    /// <summary>
    /// incoming_qty - float  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("incoming_qty")]
    public double? IncomingQty { get; set; }

    /// <summary>
    /// outgoing_qty - float  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("outgoing_qty")]
    public double? OutgoingQty { get; set; }

    /// <summary>
    /// location_id - many2one - stock.location <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("location_id")]
    public long? LocationId { get; set; }

    /// <summary>
    /// warehouse_id - many2one - stock.warehouse <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("warehouse_id")]
    public long? WarehouseId { get; set; }

    /// <summary>
    /// has_available_route_ids - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("has_available_route_ids")]
    public bool? HasAvailableRouteIds { get; set; }

    /// <summary>
    /// nbr_reordering_rules - integer  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("nbr_reordering_rules")]
    public int? NbrReorderingRules { get; set; }

    /// <summary>
    /// reordering_min_qty - float  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("reordering_min_qty")]
    public double? ReorderingMinQty { get; set; }

    /// <summary>
    /// reordering_max_qty - float  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("reordering_max_qty")]
    public double? ReorderingMaxQty { get; set; }

    /// <summary>
    /// route_from_categ_ids - many2many - stock.location.route <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("route_from_categ_ids")]
    public long[] RouteFromCategIds { get; set; }

    /// <summary>
    /// show_on_hand_qty_status_button - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("show_on_hand_qty_status_button")]
    public bool? ShowOnHandQtyStatusButton { get; set; }

    /// <summary>
    /// property_account_creditor_price_difference - many2one - account.account <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// Help: This account is used in automated inventory valuation to record the price difference between a purchase order and its related vendor bill when validating this vendor bill. <br />
    /// </summary>
    [JsonProperty("property_account_creditor_price_difference")]
    public long? PropertyAccountCreditorPriceDifference { get; set; }

    /// <summary>
    /// purchased_product_qty - float  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("purchased_product_qty")]
    public double? PurchasedProductQty { get; set; }

    /// <summary>
    /// purchase_method - selection  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: On ordered quantities: Control bills based on ordered quantities.; On received quantities: Control bills based on received quantities. <br />
    /// </summary>
    [JsonProperty("purchase_method")]
    public ControlPolicyProductTemplateOdooEnum? PurchaseMethod { get; set; }

    /// <summary>
    /// purchase_line_warn - selection  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Selecting the "Warning" option will notify user with the message, Selecting "Blocking Message" will throw an exception with the message and block the flow. The Message has to be written in the next field. <br />
    /// </summary>
    [JsonProperty("purchase_line_warn")]
    public PurchaseOrderLineWarningProductTemplateOdooEnum PurchaseLineWarn { get; set; }

    /// <summary>
    /// purchase_line_warn_msg - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("purchase_line_warn_msg")]
    public string PurchaseLineWarnMsg { get; set; }

    /// <summary>
    /// cost_method - selection  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Standard Price: The products are valued at their standard cost defined on the product.;         Average Cost (AVCO): The products are valued at weighted average cost.;         First In First Out (FIFO): The products are valued supposing those that enter the company first will also leave it first.;          <br />
    /// </summary>
    [JsonProperty("cost_method")]
    public CostingMethodProductTemplateOdooEnum? CostMethod { get; set; }

    /// <summary>
    /// valuation - selection  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Manual: The accounting entries to value the inventory are not posted automatically.;         Automated: An accounting entry is automatically created to value the inventory when a product enters or leaves the company.;          <br />
    /// </summary>
    [JsonProperty("valuation")]
    public InventoryValuationProductTemplateOdooEnum? Valuation { get; set; }

    /// <summary>
    /// route_ids - many2many - stock.location.route <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: Depending on the modules installed, this will allow you to define the route of the product: whether it will be bought, manufactured, replenished on order, etc. <br />
    /// </summary>
    [JsonProperty("route_ids")]
    public long[] RouteIds { get; set; }

    /// <summary>
    /// service_type - selection  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Manually set quantities on order: Invoice based on the manually entered quantity, without creating an analytic account.; Timesheets on contract: Invoice based on the tracked hours on the related timesheet.; Create a task and track hours: Create a task on the sales order validation and track the work hours. <br />
    /// </summary>
    [JsonProperty("service_type")]
    public TrackServiceProductTemplateOdooEnum? ServiceType { get; set; }

    /// <summary>
    /// sale_line_warn - selection  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Selecting the "Warning" option will notify user with the message, Selecting "Blocking Message" will throw an exception with the message and block the flow. The Message has to be written in the next field. <br />
    /// </summary>
    [JsonProperty("sale_line_warn")]
    public SalesOrderLineProductTemplateOdooEnum SaleLineWarn { get; set; }

    /// <summary>
    /// sale_line_warn_msg - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("sale_line_warn_msg")]
    public string SaleLineWarnMsg { get; set; }

    /// <summary>
    /// expense_policy - selection  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Expenses and vendor bills can be re-invoiced to a customer.With this option, a validated expense can be re-invoice to a customer at its cost or sales price. <br />
    /// </summary>
    [JsonProperty("expense_policy")]
    public ReInvoiceExpensesProductTemplateOdooEnum? ExpensePolicy { get; set; }

    /// <summary>
    /// visible_expense_policy - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("visible_expense_policy")]
    public bool? VisibleExpensePolicy { get; set; }

    /// <summary>
    /// sales_count - float  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("sales_count")]
    public double? SalesCount { get; set; }

    /// <summary>
    /// visible_qty_configurator - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("visible_qty_configurator")]
    public bool? VisibleQtyConfigurator { get; set; }

    /// <summary>
    /// invoice_policy - selection  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Ordered Quantity: Invoice quantities ordered by the customer.; Delivered Quantity: Invoice quantities delivered to the customer. <br />
    /// </summary>
    [JsonProperty("invoice_policy")]
    public InvoicingPolicyProductTemplateOdooEnum? InvoicePolicy { get; set; }

    /// <summary>
    /// image_1920 - binary  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("image_1920")]
    public string Image1920 { get; set; }

    /// <summary>
    /// activity_ids - one2many - mail.activity (res_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("activity_ids")]
    public long[] ActivityIds { get; set; }

    /// <summary>
    /// activity_state - selection  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Status based on activities; Overdue: Due date is already passed; Today: Activity date is today; Planned: Future activities. <br />
    /// </summary>
    [JsonProperty("activity_state")]
    public ActivityStateProductTemplateOdooEnum? ActivityState { get; set; }

    /// <summary>
    /// activity_date_deadline - date  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("activity_date_deadline")]
    public DateTime? ActivityDateDeadline { get; set; }

    /// <summary>
    /// my_activity_date_deadline - date  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("my_activity_date_deadline")]
    public DateTime? MyActivityDateDeadline { get; set; }

    /// <summary>
    /// activity_exception_decoration - selection  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Type of the exception activity on record. <br />
    /// </summary>
    [JsonProperty("activity_exception_decoration")]
    public ActivityExceptionDecorationProductTemplateOdooEnum? ActivityExceptionDecoration { get; set; }

    /// <summary>
    /// activity_exception_icon - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Icon to indicate an exception activity. <br />
    /// </summary>
    [JsonProperty("activity_exception_icon")]
    public string ActivityExceptionIcon { get; set; }

    /// <summary>
    /// message_is_follower - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("message_is_follower")]
    public bool? MessageIsFollower { get; set; }

    /// <summary>
    /// message_follower_ids - one2many - mail.followers (res_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("message_follower_ids")]
    public long[] MessageFollowerIds { get; set; }

    /// <summary>
    /// message_partner_ids - many2many - res.partner <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("message_partner_ids")]
    public long[] MessagePartnerIds { get; set; }

    /// <summary>
    /// message_channel_ids - many2many - mail.channel <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("message_channel_ids")]
    public long[] MessageChannelIds { get; set; }

    /// <summary>
    /// message_ids - one2many - mail.message (res_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("message_ids")]
    public long[] MessageIds { get; set; }

    /// <summary>
    /// message_unread - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: If checked, new messages require your attention. <br />
    /// </summary>
    [JsonProperty("message_unread")]
    public bool? MessageUnread { get; set; }

    /// <summary>
    /// message_unread_counter - integer  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Number of unread messages <br />
    /// </summary>
    [JsonProperty("message_unread_counter")]
    public int? MessageUnreadCounter { get; set; }

    /// <summary>
    /// message_needaction - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: If checked, new messages require your attention. <br />
    /// </summary>
    [JsonProperty("message_needaction")]
    public bool? MessageNeedaction { get; set; }

    /// <summary>
    /// message_needaction_counter - integer  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Number of messages which requires an action <br />
    /// </summary>
    [JsonProperty("message_needaction_counter")]
    public int? MessageNeedactionCounter { get; set; }

    /// <summary>
    /// message_has_error - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: If checked, some messages have a delivery error. <br />
    /// </summary>
    [JsonProperty("message_has_error")]
    public bool? MessageHasError { get; set; }

    /// <summary>
    /// message_has_error_counter - integer  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Number of messages with delivery error <br />
    /// </summary>
    [JsonProperty("message_has_error_counter")]
    public int? MessageHasErrorCounter { get; set; }

    /// <summary>
    /// message_attachment_count - integer  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("message_attachment_count")]
    public int? MessageAttachmentCount { get; set; }

    /// <summary>
    /// message_main_attachment_id - many2one - ir.attachment <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("message_main_attachment_id")]
    public long? MessageMainAttachmentId { get; set; }

    /// <summary>
    /// website_message_ids - one2many - mail.message (res_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: Website communication history <br />
    /// </summary>
    [JsonProperty("website_message_ids")]
    public long[] WebsiteMessageIds { get; set; }

    /// <summary>
    /// message_has_sms_error - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: If checked, some messages have a delivery error. <br />
    /// </summary>
    [JsonProperty("message_has_sms_error")]
    public bool? MessageHasSmsError { get; set; }

    /// <summary>
    /// rating_ids - one2many - rating.rating (res_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("rating_ids")]
    public long[] RatingIds { get; set; }

    /// <summary>
    /// rating_last_value - float  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("rating_last_value")]
    public double? RatingLastValue { get; set; }

    /// <summary>
    /// rating_count - integer  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("rating_count")]
    public int? RatingCount { get; set; }

    /// <summary>
    /// rating_avg - float  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("rating_avg")]
    public double? RatingAvg { get; set; }

    /// <summary>
    /// is_seo_optimized - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("is_seo_optimized")]
    public bool? IsSeoOptimized { get; set; }

    /// <summary>
    /// website_meta_title - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("website_meta_title")]
    public string WebsiteMetaTitle { get; set; }

    /// <summary>
    /// website_meta_description - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("website_meta_description")]
    public string WebsiteMetaDescription { get; set; }

    /// <summary>
    /// website_meta_keywords - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("website_meta_keywords")]
    public string WebsiteMetaKeywords { get; set; }

    /// <summary>
    /// website_meta_og_img - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("website_meta_og_img")]
    public string WebsiteMetaOgImg { get; set; }

    /// <summary>
    /// seo_name - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("seo_name")]
    public string SeoName { get; set; }

    /// <summary>
    /// website_published - boolean  <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("website_published")]
    public bool? WebsitePublished { get; set; }

    /// <summary>
    /// website_id - many2one - website <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Restrict publishing to this website. <br />
    /// </summary>
    [JsonProperty("website_id")]
    public long? WebsiteId { get; set; }

    /// <summary>
    /// is_published - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("is_published")]
    public bool? IsPublished { get; set; }

    /// <summary>
    /// can_publish - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("can_publish")]
    public bool? CanPublish { get; set; }

    /// <summary>
    /// website_url - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: The full URL to access the document through the website. <br />
    /// </summary>
    [JsonProperty("website_url")]
    public string WebsiteUrl { get; set; }

    /// <summary>
    /// default_code2 - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("default_code2")]
    public string DefaultCode2 { get; set; }

    /// <summary>
    /// image_1024 - binary  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("image_1024")]
    public string Image1024 { get; set; }

    /// <summary>
    /// image_512 - binary  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("image_512")]
    public string Image512 { get; set; }

    /// <summary>
    /// image_256 - binary  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("image_256")]
    public string Image256 { get; set; }

    /// <summary>
    /// image_128 - binary  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("image_128")]
    public string Image128 { get; set; }

    /// <summary>
    /// activity_user_id - many2one - res.users <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("activity_user_id")]
    public long? ActivityUserId { get; set; }

    /// <summary>
    /// activity_type_id - many2one - mail.activity.type <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("activity_type_id")]
    public long? ActivityTypeId { get; set; }

    /// <summary>
    /// activity_type_icon - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Font awesome icon e.g. fa-tasks <br />
    /// </summary>
    [JsonProperty("activity_type_icon")]
    public string ActivityTypeIcon { get; set; }

    /// <summary>
    /// activity_summary - char  <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("activity_summary")]
    public string ActivitySummary { get; set; }

    /// <summary>
    /// rating_last_feedback - text  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// Help: Reason of the rating <br />
    /// </summary>
    [JsonProperty("rating_last_feedback")]
    public string RatingLastFeedback { get; set; }

    /// <summary>
    /// rating_last_image - binary  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("rating_last_image")]
    public string RatingLastImage { get; set; }

    /// <summary>
    /// optional_product_ids - many2many - product.template <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: Optional Products are suggested whenever the customer hits *Add to Cart* (cross-sell strategy, e.g. for computers: warranty, software, etc.). <br />
    /// </summary>
    [JsonProperty("optional_product_ids")]
    public long[] OptionalProductIds { get; set; }

    /// <summary>
    /// service_to_purchase - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: If ticked, each time you sell this product through a SO, a RfQ is automatically created to buy the product. Tip: don't forget to set a vendor on the product. <br />
    /// </summary>
    [JsonProperty("service_to_purchase")]
    public bool? ServiceToPurchase { get; set; }

    /// <summary>
    /// website_description - html  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("website_description")]
    public string WebsiteDescription { get; set; }

    /// <summary>
    /// alternative_product_ids - many2many - product.template <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: Suggest alternatives to your customer (upsell strategy). Those products show up on the product page. <br />
    /// </summary>
    [JsonProperty("alternative_product_ids")]
    public long[] AlternativeProductIds { get; set; }

    /// <summary>
    /// accessory_product_ids - many2many - product.product <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: Accessories show up when the customer reviews the cart before payment (cross-sell strategy). <br />
    /// </summary>
    [JsonProperty("accessory_product_ids")]
    public long[] AccessoryProductIds { get; set; }

    /// <summary>
    /// website_size_x - integer  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("website_size_x")]
    public int? WebsiteSizeX { get; set; }

    /// <summary>
    /// website_size_y - integer  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("website_size_y")]
    public int? WebsiteSizeY { get; set; }

    /// <summary>
    /// website_ribbon_id - many2one - product.ribbon <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("website_ribbon_id")]
    public long? WebsiteRibbonId { get; set; }

    /// <summary>
    /// website_sequence - integer  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Determine the display order in the Website E-commerce <br />
    /// </summary>
    [JsonProperty("website_sequence")]
    public int? WebsiteSequence { get; set; }

    /// <summary>
    /// public_categ_ids - many2many - product.public.category <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: The product will be available in each mentioned eCommerce category. Go to Shop > Customize and enable 'eCommerce categories' to view all eCommerce categories. <br />
    /// </summary>
    [JsonProperty("public_categ_ids")]
    public long[] PublicCategIds { get; set; }

    /// <summary>
    /// product_template_image_ids - one2many - product.image (product_tmpl_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("product_template_image_ids")]
    public long[] ProductTemplateImageIds { get; set; }

    /// <summary>
    /// website_hide_price - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("website_hide_price")]
    public bool? WebsiteHidePrice { get; set; }

    /// <summary>
    /// website_hide_price_message - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: When the price is hidden on the website we can give the customersome tips on how to find it out. <br />
    /// </summary>
    [JsonProperty("website_hide_price_message")]
    public string WebsiteHidePriceMessage { get; set; }

    /// <summary>
    /// inventory_availability - selection  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Adds an inventory availability status on the web product page. <br />
    /// </summary>
    [JsonProperty("inventory_availability")]
    public InventoryAvailabilityProductTemplateOdooEnum? InventoryAvailability { get; set; }

    /// <summary>
    /// available_threshold - float  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("available_threshold")]
    public double? AvailableThreshold { get; set; }

    /// <summary>
    /// custom_message - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("custom_message")]
    public string CustomMessage { get; set; }

    /// <summary>
    /// dr_label_id - many2one - dr.product.label <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("dr_label_id")]
    public long? DrLabelId { get; set; }

    /// <summary>
    /// dr_brand_id - many2one - dr.product.brand <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("dr_brand_id")]
    public long? DrBrandId { get; set; }

    /// <summary>
    /// dr_tag_ids - many2many - dr.product.tags <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("dr_tag_ids")]
    public long[] DrTagIds { get; set; }

    /// <summary>
    /// dr_tab_ids - one2many - dr.product.tabs (product_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: Display in product detail page on website. <br />
    /// </summary>
    [JsonProperty("dr_tab_ids")]
    public long[] DrTabIds { get; set; }

    /// <summary>
    /// dr_offer_ids - one2many - dr.product.offer (product_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: Display in product detail page on website. <br />
    /// </summary>
    [JsonProperty("dr_offer_ids")]
    public long[] DrOfferIds { get; set; }

    /// <summary>
    /// label_line_ids - one2many - product.label.line (product_tmpl_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: Set the number of product labels <br />
    /// </summary>
    [JsonProperty("label_line_ids")]
    public long[] LabelLineIds { get; set; }

    /// <summary>
    /// product_brand_ept_id - many2one - product.brand.ept <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Select a brand for this product <br />
    /// </summary>
    [JsonProperty("product_brand_ept_id")]
    public long? ProductBrandEptId { get; set; }

    /// <summary>
    /// tab_line_ids - one2many - product.tab.line (product_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// Help: Set the product tabs <br />
    /// </summary>
    [JsonProperty("tab_line_ids")]
    public long[] TabLineIds { get; set; }

    /// <summary>
    /// document_ids - many2many - ir.attachment <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("document_ids")]
    public long[] DocumentIds { get; set; }

    /// <summary>
    /// dr_has_discount - boolean  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("dr_has_discount")]
    public bool? DrHasDiscount { get; set; }

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