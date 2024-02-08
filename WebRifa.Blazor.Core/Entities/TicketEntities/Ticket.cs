using WebRifa.Blazor.Core.Enums;
using WebRifa.Blazor.Core.Factories;
using WebRifa.Blazor.Core.Interfaces.States;

namespace WebRifa.Blazor.Core.Entities.TicketEntities;
public class Ticket : BaseEntity
{
    public int Number { get; private set; }
    public string Observations { get; private set; } = string.Empty;

    public TicketStates CurrentState { get; private set; }
    public ITicketState State { get; private set; } = new TicketValidState();

    public List<BuyerTicketReceipt>? BuyerTicketReceipt { get; private set; }

    public Guid RaffleId { get; private set; } 
    public Raffle? Raffle { get; private set; }

    public Guid? DrawId { get; private set; }
    public Draw? Draw { get; private set; }
    public Receipt? Receipt { get; private set; }

    public Ticket()
    {
        State = CurrentState.GetTicketState();
    }

    public Ticket(
        int number,
        string observations,
        Guid raffleId)
    {
        Number = number;
        Observations = observations;
        RaffleId = raffleId;
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

    public void ChangeState(TicketStates ticketState)
    {
        CurrentState = ticketState;
        SetUpdatedAt();
    }

    public void AddDraw(DrawEntities.Draw draw)
    {
        Draw = draw;
    }
}