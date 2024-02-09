using WebRifa.Blazor.Core.Enums;
using WebRifa.Blazor.Core.Interfaces.States;

namespace WebRifa.Blazor.Core.Dtos;
public class TicketDto {
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string Observations { get; set; } = string.Empty;
    public List<BuyerTicketReceiptDto> BuyerTicketReceipt { get; set; } = new();
}