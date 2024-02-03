using Newtonsoft.Json;
using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;
using StoneAssemblies.OdooBot.Models;

namespace StoneAssemblies.OdooBot.Tests;

[OdooTableName("ir.attachment")]
[JsonConverter(typeof(OdooModelConverter))]
public class IrAttachmentOdooModel : IOdooModel
{
    /// <summary>
    /// name - char  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// description - text  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    /// res_name - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("res_name")]
    public string ResName { get; set; }

    /// <summary>
    /// res_model - char  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// Help: The database object this attachment will be attached to. <br />
    /// </summary>
    [JsonProperty("res_model")]
    public string ResModel { get; set; }

    /// <summary>
    /// res_field - char  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("res_field")]
    public string ResField { get; set; }

    /// <summary>
    /// res_id - many2one_reference  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// Help: The record id this is attached to. <br />
    /// </summary>
    [JsonProperty("res_id")]
    public long? ResId { get; set; }

    /// <summary>
    /// company_id - many2one - res.company <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("company_id")]
    public long? CompanyId { get; set; }

    /// <summary>
    /// type - selection  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// Help: You can either upload a file from your computer or copy/paste an internet link to your file. <br />
    /// </summary>
    [JsonProperty("type")]
    public TypeIrAttachmentOdooEnum Type { get; set; }

    /// <summary>
    /// url - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; }

    /// <summary>
    /// public - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("public")]
    public bool? Public { get; set; }

    /// <summary>
    /// access_token - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    /// raw - binary  <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("raw")]
    public string Raw { get; set; }

    /// <summary>
    /// datas - binary  <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("datas")]
    public string Datas { get; set; }

    /// <summary>
    /// db_datas - binary  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("db_datas")]
    public string DbDatas { get; set; }

    /// <summary>
    /// store_fname - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("store_fname")]
    public string StoreFname { get; set; }

    /// <summary>
    /// file_size - integer  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("file_size")]
    public int? FileSize { get; set; }

    /// <summary>
    /// checksum - char  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("checksum")]
    public string Checksum { get; set; }

    /// <summary>
    /// mimetype - char  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("mimetype")]
    public string Mimetype { get; set; }

    /// <summary>
    /// index_content - text  <br />
    /// Required: False, Readonly: True, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("index_content")]
    public string IndexContent { get; set; }

    /// <summary>
    /// local_url - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("local_url")]
    public string LocalUrl { get; set; }

    /// <summary>
    /// image_src - char  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("image_src")]
    public string ImageSrc { get; set; }

    /// <summary>
    /// image_width - integer  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("image_width")]
    public int? ImageWidth { get; set; }

    /// <summary>
    /// image_height - integer  <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("image_height")]
    public int? ImageHeight { get; set; }

    /// <summary>
    /// original_id - many2one - ir.attachment <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("original_id")]
    public long? OriginalId { get; set; }

    /// <summary>
    /// website_url - char  <br />
    /// Required: False, Readonly: False, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("website_url")]
    public string WebsiteUrl { get; set; }

    /// <summary>
    /// website_id - many2one - website <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("website_id")]
    public long? WebsiteId { get; set; }

    /// <summary>
    /// key - char  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Technical field used to resolve multiple attachments in a multi-website environment. <br />
    /// </summary>
    [JsonProperty("key")]
    public string Key { get; set; }

    /// <summary>
    /// theme_template_id - many2one - theme.ir.attachment <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("theme_template_id")]
    public long? ThemeTemplateId { get; set; }

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