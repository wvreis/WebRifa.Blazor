namespace WebRifa.Blazor.Core.Entities;
public class Raffle : BaseEntity {

    public string Description { get; private set; } = string.Empty;
    public int TotalNumberOfTickets { get; private set; }
    public decimal TicketPrice { get; private set; }
    public string Observations { get; private set; } = string.Empty;
    public DateTime DrawDateTime { get; set; }

    public List<Ticket>? Tickets { get; private set; }


    public Raffle()
    {
        
    }

    public Raffle(
        string description,
        int totalNumberOfTickets,
        decimal ticketPrice,
        string observations,
        DateTime drawDateTime,
        List<Ticket>? tickets)
    {
        Description = description;
        TotalNumberOfTickets = totalNumberOfTickets;
        TicketPrice = ticketPrice;
        Observations = observations;
        DrawDateTime = drawDateTime;
        Tickets = tickets;
    }
}