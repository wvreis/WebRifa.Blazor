using WebRifa.Blazor.Core.Interfaces.ApplicationModels;

namespace WebRifa.Blazor.Core.ApplicationModels;

public class PaginatedList<T> : IPaginatedList<T> {
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public int PageSize { get; init; }
    public List<T> Items { get; init; } = new();

    public PaginatedList()
    {
        
    }

    public PaginatedList(
        int currentPage,
        int totalPages,
        int pageSize,
        List<T> items)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        PageSize = pageSize;
        Items = items;
    }
}
