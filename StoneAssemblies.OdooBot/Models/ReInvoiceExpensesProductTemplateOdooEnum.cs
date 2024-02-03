using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Models;

/// <summary>
/// Help: Expenses and vendor bills can be re-invoiced to a customer.With this option, a validated expense can be re-invoice to a customer at its cost or sales price.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ReInvoiceExpensesProductTemplateOdooEnum
{
    [EnumMember(Value = "no")] No = 1,

    [EnumMember(Value = "cost")] AtCost = 2,

    [EnumMember(Value = "sales_price")] SalesPrice = 3
}