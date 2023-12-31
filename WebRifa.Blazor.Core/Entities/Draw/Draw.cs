namespace WebRifa.Blazor.Core.Entities.Draw;
public class Draw : BaseEntity {
    public DateTime RaffledAt { get; private set; } = DateTime.UtcNow;

    public Ticket.Ticket? RaffledTicket { get; set; }

    public Draw() { }

    public Draw(Ticket.Ticket ticket)
    {
        RaffledTicket = ticket;
    }
}