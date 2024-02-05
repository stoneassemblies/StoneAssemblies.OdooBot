using System.ComponentModel.DataAnnotations;

namespace StoneAssemblies.OdooBot.Entities;

public class Category
{
    [Key]
    public Guid Id { get; set; }

    public long ExternalId { get; set; }

    public string Name { get; set; }

    public List<Product> Products { get; set; }
}