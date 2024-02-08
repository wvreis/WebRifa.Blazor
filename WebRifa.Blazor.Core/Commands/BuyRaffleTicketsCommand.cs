namespace WebRifa.Blazor.Core.Commands;
public class BuyRaffleTicketsCommand {
    public Guid RaffleId { get; set; }
    public Guid BuyerId { get; set; }
    public List<int> NumbersToBuy { get; set; } = new();
}