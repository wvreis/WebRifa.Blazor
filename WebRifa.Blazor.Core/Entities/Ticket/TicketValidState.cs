using WebRifa.Blazor.Core.Interfaces.States;

namespace WebRifa.Blazor.Core.Entities.Ticket;
public class TicketValidState : ITicketState
{
    public void Cancel(Ticket ticket)
    {
        ticket.ChangeState(Enums.TicketStates.Canceled);
    }

    public void MarkAsLoser(Ticket ticket)
    {
        ticket.ChangeState(Enums.TicketStates.Loser);
    }

    public void MarkAsWinner(Ticket ticket)
    {
        ticket.ChangeState(Enums.TicketStates.Winner);
    }

    public void UnmarkAsLoser(Ticket ticket)
    {
        ticket.ChangeState(Enums.TicketStates.Valid);
    }

    public void UnmarkAsWinner(Ticket ticket)
    {
        ticket.ChangeState(Enums.TicketStates.Valid);
    }
}