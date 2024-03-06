namespace WebRifa.Blazor.Core.Interfaces.ApplicationModels;

public interface IPaginatedList<T> {
    int CurrentPage { get; init; }
    int TotalPages { get; init; }
    int PageSize { get; init; }
    List<T> Items { get; init; }
}
