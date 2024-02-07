using WebRifa.Blazor.Core.Entities.TicketEntities;

namespace WebRifa.Blazor.Core.Interfaces.States;

public interface ITicketState
{
    void Cancel(Ticket ticket);
    void MarkAsWinner(Ticket ticket);
    void MarkAsLoser(Ticket ticket);
    void UnmarkAsWinner(Ticket ticket);
    void UnmarkAsLoser(Ticket ticket);
}
