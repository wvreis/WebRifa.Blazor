using WebRifa.Blazor.Components.Common.PopUp;
using WebRifa.Blazor.Core.Dtos;

namespace WebRifa.Blazor.Components.Pages.Draws;
public partial class DrawsIndex {
    public List<DrawDto>? Draws { get; set; }

    PopUp? PopUp { get; set; }

    protected override async Task OnInitializedAsync()
    {
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

        Draws = await drawService.GetAllDrawsAsync();
    }

    bool IsPopUpVisible() => 
        PopUp is not null && PopUp.IsVisible;

    string GetNumberAsString(DrawDto draw) => 
        draw.DrawnTicketNumber.ToString();
}