﻿using WebRifa.Blazor.Core.Interfaces.States;

namespace WebRifa.Blazor.Core.Entities.TicketEntities;
public class TicketWinnerState : ITicketState
{
    public void Cancel(Ticket ticket)
    {
        throw new Exception("Não é possível Cancelar o Bilhete, pois o Sorteio já foi realizado.");
    }

    public void MarkAsLoser(Ticket ticket)
    {
        throw new Exception("Não é possível, pois o Sorteio já foi realizado.");
    }

    public void MarkAsWinner(Ticket ticket)
    {
        throw new Exception("O Bilhete já está marcado como Vencedor.");
    }

    public void UnmarkAsLoser(Ticket ticket)
    {
        throw new Exception("Não é possível, pois o Sorteio já foi realizado.");
    }

    public void UnmarkAsWinner(Ticket ticket)
    {
        ticket.ChangeState(Enums.TicketStates.Valid);
    }
}