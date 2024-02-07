namespace WebRifa.Blazor.Core.Entities.DrawEntities;
public class Draw : BaseEntity {
    public DateTime RaffledAt { get; private set; } = DateTime.UtcNow;

    public Guid RaffleId { get; private set; }
    public Raffle? Raffle { get; private set; }

    public Guid DrawnTicketId { get; private set; }
    public TicketEntities.Ticket? DrawnTicket { get; private set; }

    public Draw() { }

    public Draw(TicketEntities.Ticket ticket, Raffle raffle)
    {
        Raffle = raffle;
        DrawnTicket = ticket;
    }

    public void CarryOutTheDraw()
    {

    }
}