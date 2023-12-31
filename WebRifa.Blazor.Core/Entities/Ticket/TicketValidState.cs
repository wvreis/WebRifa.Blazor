using WebRifa.Blazor.Core.Interfaces;

namespace WebRifa.Blazor.Core.Entities.Ticket;
public class TicketValidState : ITicketState
{
    public void Cancel(Ticket ticket)
    {
        ticket.ChangeState(Enums.TicketState.Canceled);
    }

    public void MarkAsLoser(Ticket ticket)
    {
        ticket.ChangeState(Enums.TicketState.Loser);
    }

    public void MarkAsWinner(Ticket ticket)
    {
        ticket.ChangeState(Enums.TicketState.Winner);
    }

    public void UnmarkAsLoser(Ticket ticket)
    {
        ticket.ChangeState(Enums.TicketState.Valid);
    }

    public void UnmarkAsWinner(Ticket ticket)
    {
        ticket.ChangeState(Enums.TicketState.Valid);
    }
}