namespace StoneAssemblies.OdooBot.Models;

using Newtonsoft.Json;

using PortaCapena.OdooJsonRpcClient.Attributes;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;

[OdooTableName("product.public.category")]
[JsonConverter(typeof(OdooModelConverter))]
public class ProductPublicCategoryOdooModel : IOdooModel
{
    /// <summary>
    /// image_1920 - binary  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("image_1920")]
    public string Image1920 { get; set; }

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
    /// website_id - many2one - website <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Restrict publishing to this website. <br />
    /// </summary>
    [JsonProperty("website_id")]
    public long? WebsiteId { get; set; }

    /// <summary>
    /// name - char  <br />
    /// Required: True, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// parent_id - many2one - product.public.category <br />
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
    /// child_id - one2many - product.public.category (parent_id) <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("child_id")]
    public long[] ChildId { get; set; }

    /// <summary>
    /// parents_and_self - many2many - product.public.category <br />
    /// Required: False, Readonly: True, Store: False, Sortable: False <br />
    /// </summary>
    [JsonProperty("parents_and_self")]
    public long[] ParentsAndSelf { get; set; }

    /// <summary>
    /// sequence - integer  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Gives the sequence order when displaying a list of product categories. <br />
    /// </summary>
    [JsonProperty("sequence")]
    public int? Sequence { get; set; }

    /// <summary>
    /// website_description - html  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("website_description")]
    public string WebsiteDescription { get; set; }

    /// <summary>
    /// product_tmpl_ids - many2many - product.template <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("product_tmpl_ids")]
    public long[] ProductTmplIds { get; set; }

    /// <summary>
    /// dr_category_label_id - many2one - dr.product.public.category.label <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// </summary>
    [JsonProperty("dr_category_label_id")]
    public long? DrCategoryLabelId { get; set; }

    /// <summary>
    /// dr_category_cover_image - binary  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: False <br />
    /// </summary>
    [JsonProperty("dr_category_cover_image")]
    public string DrCategoryCoverImage { get; set; }

    /// <summary>
    /// allow_in_category_carousel - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: You can set this category in category carousel snippets. <br />
    /// </summary>
    [JsonProperty("allow_in_category_carousel")]
    public bool? AllowInCategoryCarousel { get; set; }

    /// <summary>
    /// is_category_page - boolean  <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: It will set the separate page for this category <br />
    /// </summary>
    [JsonProperty("is_category_page")]
    public bool? IsCategoryPage { get; set; }

    /// <summary>
    /// category_page - many2one - website.page <br />
    /// Required: False, Readonly: False, Store: True, Sortable: True <br />
    /// Help: Select the page which you want to set for this category. <br />
    /// </summary>
    [JsonProperty("category_page")]
    public long? CategoryPage { get; set; }

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