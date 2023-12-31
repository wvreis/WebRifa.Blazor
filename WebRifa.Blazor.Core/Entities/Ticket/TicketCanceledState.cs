using WebRifa.Blazor.Core.Interfaces.States;

namespace WebRifa.Blazor.Core.Entities.Ticket;
public class TicketCanceledState : ITicketState
{
    public void Cancel(Ticket ticket)
    {
        throw new Exception("O Bilhete já está Cancelado.");
    }

    public void MarkAsLoser(Ticket ticket)
    {
        throw new Exception("Não é possível, pois o Bilhete está cancelado.");
    }

    public void MarkAsWinner(Ticket ticket)
    {
        throw new Exception("Não é possível, pois o Bilhete está cancelado.");
    }

    public void UnmarkAsLoser(Ticket ticket)
    {
        throw new Exception("Não é possível, pois o Bilhete está cancelado.");
    }

    public void UnmarkAsWinner(Ticket ticket)
    {
        throw new Exception("Não é possível, pois o Bilhete está cancelado.");
    }
}