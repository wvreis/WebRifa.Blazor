namespace WebRifa.Blazor.Core.Requests.Commands;
public class BuyRaffleTicketsCommand
{
    public Guid RaffleId { get; set; }
    public Guid BuyerId { get; set; }
    public HashSet<int> NumbersToBuy { get; set; } = new();
    public string Observations { get; set; } = string.Empty;
}