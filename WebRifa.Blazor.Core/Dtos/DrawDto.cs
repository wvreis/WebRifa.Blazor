namespace WebRifa.Blazor.Core.Dtos;
public class DrawDto {
    public Guid Id { get; set; }
    public string RaffleDescription { get; set; } = string.Empty;
    public int DrawnTicketNumber { get; set; }
    public string DrawnTicketBuyerName { get; set; } = string.Empty;
    public DateTime RaffledAt { get; set; }
}