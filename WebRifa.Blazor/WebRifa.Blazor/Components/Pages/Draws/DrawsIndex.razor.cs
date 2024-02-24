using WebRifa.Blazor.Core.Dtos;

namespace WebRifa.Blazor.Components.Pages.Draws;
public partial class DrawsIndex {
    public List<DrawDto>? Draws { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Draws = await drawService.GetAllDrawsAsync();
    }

    string GetNumberAsString(DrawDto draw) => 
        draw.DrawnTicketNumber.ToString();
}