namespace WebRifa.Blazor.Core.Dtos;

public class ReceiptDto {
    public Guid Id { get; set; }
    public Guid BuyerId { get; set; }
    public Guid RaffleId { get; set; }
    public string BuyerName { get; set; } = string.Empty;
    public string RaffleDescription { get; set;} = string.Empty;
    public List<int> TicketsNumbers { get; set; } = new();
}   