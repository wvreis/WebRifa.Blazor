namespace WebRifa.Blazor.Core.Dtos;
public class RaffleDto {
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int TotalNumberOfTickets { get; set; }
    public decimal TicketPrice { get; set; }
    public string Observations { get; set; } = string.Empty;
    public DateTime DrawDateTime { get; set; }
    public List<Ticket>? Tickets { get; set; }
}