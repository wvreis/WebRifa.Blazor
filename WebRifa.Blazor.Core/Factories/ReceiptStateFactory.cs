using WebRifa.Blazor.Core.Enums;
using WebRifa.Blazor.Core.Interfaces.States;

namespace WebRifa.Blazor.Core.Factories;
public static class ReceiptStateFactory {
    public static IReceiptState GetReceiptState(this ReceiptStates receiptState)
    {
        switch (receiptState) {
            case ReceiptStates.Valid:
                return new ReceiptValidState();
            case ReceiptStates.Canceled:
                return new ReceiptCanceledState();
            default:
                throw new ArgumentException("Choose a valid option.");
        }
    }
}