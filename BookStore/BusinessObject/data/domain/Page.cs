public class Page<T>
{
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);
    public List<T> Items { get; set; }

    public Page(List<T> items, int totalItems, int pageSize, int currentPage)
    {
        Items = items;
        TotalItems = totalItems;
        PageSize = pageSize;
        CurrentPage = currentPage;
    }
}