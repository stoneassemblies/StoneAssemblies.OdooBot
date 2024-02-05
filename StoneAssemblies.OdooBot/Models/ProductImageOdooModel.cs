using Newtonsoft.Json;
using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;

namespace StoneAssemblies.OdooBot.Models;

[OdooTableName("product.image")]
[JsonConverter(typeof(OdooModelConverter))]
public class ProductImageOdooModel : IOdooModel
{
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
    /// name - char  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// sequence - integer  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("sequence")]
    public int? Sequence { get; set; }

    /// <summary>
    /// image_1920 - binary  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("image_1920")]
    public string Image1920 { get; set; }

    /// <summary>
    /// product_tmpl_id - many2one - product.template <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("product_tmpl_id")]
    public long? ProductTmplId { get; set; }

    /// <summary>
    /// product_variant_id - many2one - product.product <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("product_variant_id")]
    public long? ProductVariantId { get; set; }

    /// <summary>
    /// video_url - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: URL of a video for showcasing your product. <br />
    /// </summary>
    [JsonProperty("video_url")]
    public string VideoUrl { get; set; }

    /// <summary>
    /// embed_code - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("embed_code")]
    public string EmbedCode { get; set; }

    /// <summary>
    /// can_image_1024_be_zoomed - boolean  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("can_image_1024_be_zoomed")]
    public bool? CanImage1024BeZoomed { get; set; }

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