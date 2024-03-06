using Microsoft.AspNetCore.Components;
using WebRifa.Blazor.Components.Common.PopUp;
using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Queries.Draw;

namespace WebRifa.Blazor.Components.Pages.Draws;
public partial class DrawsIndex {
    [Parameter]
    public int CurrentPage { get; set; }

    public PaginatedList<DrawDto>? Draws { get; set; }

    PopUp? PopUp { get; set; }
    DrawGetAllQuery Query { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Query.CurrentPage = CurrentPage;
        await LoadDraws();
    }
    async Task TogglePopUp()
    {
        if (PopUp is not null) {
            await PopUp.ToggleVisibilityAsync();
        }
    }

    async Task LoadDraws()
    {
        if (IsPopUpVisible()) {
            return;
        }

        Draws = await drawService.GetAllDrawsAsync(Query);
    }

    bool IsPopUpVisible() => 
        PopUp is not null && PopUp.IsVisible;

    string GetNumberAsString(DrawDto draw) => 
        draw.DrawnTicketNumber.ToString();
}