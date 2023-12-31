﻿using WebRifa.Blazor.Core.Interfaces.States;

namespace WebRifa.Blazor.Core.Entities.Receipt;
public class ReceiptCanceledState : IReceiptState {
    public void Cancel(Receipt receipt)
    {
        throw new Exception("O Recibo já está Cancelado.");
    }
}