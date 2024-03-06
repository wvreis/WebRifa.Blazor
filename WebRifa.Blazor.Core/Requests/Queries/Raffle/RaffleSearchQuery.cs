namespace WebRifa.Blazor.Core.Requests.Queries.Raffle;
public class RaffleSearchQuery
{
    int currentPage;
    public int CurrentPage {
        get {
            return currentPage;
        }
        set {
            if (value == 0) {
                currentPage = 1;
            }
            else {
                currentPage = value;
            }
        }
    }

    public string SearchTerm { get; set; } = string.Empty;
}