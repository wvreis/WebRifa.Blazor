using WebRifa.Blazor.Core.Interfaces;

namespace WebRifa.Blazor.Core.Entities.Ticket;
public class TicketLoserState : Ticket, ITicketState
{
    public void Cancel(Ticket ticket)
    {
        throw new Exception("Não é possível Cancelar o Bilhete, pois o Sorteio já foi realizado.");
    }

    public void MarkAsLoser(Ticket ticket)
    {
        throw new Exception("O Bilhete já está marcado como Perdedor.");
    }

    public void MarkAsWinner(Ticket ticket)
    {
        throw new Exception("Não é possível Marcar o Bilhete como Vencedor, pois o Sorteio já foi realizado.");
    }

    public void UnmarkAsLoser(Ticket ticket)
    {
        ticket.ChangeState(Enums.TicketStates.Valid);
    }

    public void UnmarkAsWinner(Ticket ticket)
    {
        throw new Exception("Não é possível Desmarcar como Vencedor, pois não está marcado como.");
    }
}