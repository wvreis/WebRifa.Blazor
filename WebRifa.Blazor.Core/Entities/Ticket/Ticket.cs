using WebRifa.Blazor.Core.Common;
using WebRifa.Blazor.Core.Enums;
using WebRifa.Blazor.Core.Factories;
using WebRifa.Blazor.Core.Interfaces;

namespace WebRifa.Blazor.Core.Entities.Ticket;
public class Ticket : BaseEntity
{
    public int Number { get; private set; }
    public string Observations { get; private set; } = string.Empty;

    public TicketState CurrentState { get; private set; }
    public ITicketState State { get; private set; } = new TicketValidState();

    public string BuyerId { get; private set; } = string.Empty;
    public Buyer? Buyer { get; private set; }

    public string RaffleId { get; private set; } = string.Empty;
    public Raffle? Raffle { get; private set; }

    public Receipt.Receipt? Receipt { get; private set; }

    public Ticket()
    {
        State = CurrentState.GetTicketState();
    }

    public void Cancel()
    {
        State.Cancel(this);
    }

    public void MarkAsWinner()
    {
        State.MarkAsWinner(this);
    }

    public void MarkAsLoser()
    {
        State.MarkAsLoser(this);
    }

    public void UnmarkAsWinner()
    {
        State.UnmarkAsWinner(this);
    }

    public void UnmarkAsLoser()
    {
        State.UnmarkAsLoser(this);
    }

    public void ChangeState(TicketState ticketState)
    {
        CurrentState = ticketState;
        UpdatedAt = DateTime.Now;
    }
}