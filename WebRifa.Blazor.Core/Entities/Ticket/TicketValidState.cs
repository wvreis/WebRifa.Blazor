using WebRifa.Blazor.Core.Interfaces;

namespace WebRifa.Blazor.Core.Entities.Ticket;
public class TicketValidState : ITicketState
{
    public void Cancel(Ticket ticket)
    {
        ticket.CurrentState = Enums.TicketState.Canceled;
    }

    public void MarkAsLoser(Ticket ticket)
    {
        ticket.CurrentState = Enums.TicketState.Loser;
    }

    public void MarkAsWinner(Ticket ticket)
    {
        ticket.CurrentState = Enums.TicketState.Winner;
    }

    public void UnmarkAsLoser(Ticket ticket)
    {
        ticket.CurrentState = Enums.TicketState.Valid;
    }

    public void UnmarkAsWinner(Ticket ticket)
    {
        ticket.CurrentState = Enums.TicketState.Valid;
    }
}