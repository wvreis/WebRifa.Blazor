using WebRifa.Blazor.Core.Common;
using WebRifa.Blazor.Core.Enums;
using WebRifa.Blazor.Core.Factories;
using WebRifa.Blazor.Core.Interfaces.States;

namespace WebRifa.Blazor.Core.Entities.ReceiptEntities;
public class Receipt : BaseEntity
{
    public ReceiptStates CurrentState { get; private set; }
    public IReceiptState State { get; private set; } = new ReceiptValidState();

    public List<BuyerTicketReceipt> BuyerTicketReceipt { get; set; } = new();

    public List<Ticket> Tickets { get; private set; } = new();

    public Receipt()
    {
        State = CurrentState.GetReceiptState();
    }

    public void Cancel()
    {
        State.Cancel(this);
    }

    public void ChangeState(ReceiptStates state)
    {
        CurrentState = state;
        SetUpdatedAt();
    }

    public void AddTicket(Ticket ticket)
    {
        Tickets.Add(ticket);
    }

    public void AddBuyerTicketReceipt(BuyerTicketReceipt buyerTicketReceipt)
    {
        BuyerTicketReceipt.Add(buyerTicketReceipt);
    }
}