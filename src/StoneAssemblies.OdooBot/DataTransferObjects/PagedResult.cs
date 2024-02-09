namespace StoneAssemblies.OdooBot.DataTransferObjects;

public class PagedResult<T>
{
    public List<T>? Items { get; set; }

    public int Count { get; set; }

    public static readonly PagedResult<T> Empty = new PagedResult<T>();
}