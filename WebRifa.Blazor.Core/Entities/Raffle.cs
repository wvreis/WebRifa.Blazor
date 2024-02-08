using WebRifa.Blazor.Core.Common;

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

    void Update(
        string? description,
        int? totalNumberOfTickets,
        decimal? ticketPrice,
        string? observations,
        DateTime? drawDateTime)
    {
        if (description is not null) {
            this.Description = description;
        }

        if (totalNumberOfTickets is not null) {
            this.TotalNumberOfTickets = totalNumberOfTickets.Value;
        }

        if (ticketPrice is not null) {
            this.TicketPrice = ticketPrice.Value;
        }

        if (observations is not null) {
            this.Observations = observations;
        }

        if (drawDateTime is not null) {
            this.DrawDateTime = drawDateTime.Value;
        }
    }
}