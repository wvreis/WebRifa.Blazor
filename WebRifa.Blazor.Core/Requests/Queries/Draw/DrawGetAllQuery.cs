namespace WebRifa.Blazor.Core.Requests.Queries.Draw;
public class DrawGetAllQuery {
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
}
