using WebRifa.Blazor.Core.Common;
using WebRifa.Blazor.Core.Enums;
using WebRifa.Blazor.Core.Factories;
using WebRifa.Blazor.Core.Interfaces.States;

namespace WebRifa.Blazor.Core.Entities.Receipt;
public class Receipt : BaseEntity
{
    public ReceiptStates CurrentState { get; private set; }
    public IReceiptState State { get; private set; } = new ReceiptValidState();

    public Guid BuyerId { get; private set; }
    public Buyer? Buyer { get; set; }

    public List<Ticket.Ticket> Tickets { get; private set; } = new();

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
        UpdatedAt = DateTime.UtcNow;
    }
}