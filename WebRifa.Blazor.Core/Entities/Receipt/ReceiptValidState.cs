using WebRifa.Blazor.Core.Enums;
using WebRifa.Blazor.Core.Interfaces;

namespace WebRifa.Blazor.Core.Entities.Receipt;
public class ReceiptValidState : IReceiptState {
    public void Cancel(Receipt receipt)
    {
        receipt.Tickets.ForEach(t => t.Cancel());
        receipt.CurrentState = ReceiptStates.Canceled;
    }
}