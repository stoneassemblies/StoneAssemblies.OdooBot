using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Tests;

/// <summary>
/// Help: Standard Price: The products are valued at their standard cost defined on the product. <br />
///         Average Cost (AVCO): The products are valued at weighted average cost. <br />
///         First In First Out (FIFO): The products are valued supposing those that enter the company first will also leave it first. <br />
///         
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum CostingMethodProductTemplateOdooEnum
{
    [EnumMember(Value = "standard")] StandardPrice = 1,

    [EnumMember(Value = "fifo")] FirstInFirstOutFIFO = 2,

    [EnumMember(Value = "average")] AverageCostAVCO = 3
}