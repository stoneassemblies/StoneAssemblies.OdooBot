using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Tests;

/// <summary>
/// Help: Ordered Quantity: Invoice quantities ordered by the customer. <br />
/// Delivered Quantity: Invoice quantities delivered to the customer.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum InvoicingPolicyProductTemplateOdooEnum
{
    [EnumMember(Value = "order")] OrderedQuantities = 1,

    [EnumMember(Value = "delivery")] DeliveredQuantities = 2
}