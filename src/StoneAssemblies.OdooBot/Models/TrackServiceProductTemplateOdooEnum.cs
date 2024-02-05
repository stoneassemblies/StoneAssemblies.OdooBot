using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace StoneAssemblies.OdooBot.Models;

/// <summary>
/// Help: Manually set quantities on order: Invoice based on the manually entered quantity, without creating an analytic account. <br />
/// Timesheets on contract: Invoice based on the tracked hours on the related timesheet. <br />
/// Create a task and track hours: Create a task on the sales order validation and track the work hours.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum TrackServiceProductTemplateOdooEnum
{
    [EnumMember(Value = "manual")] ManuallySetQuantitiesOnOrder = 1
}