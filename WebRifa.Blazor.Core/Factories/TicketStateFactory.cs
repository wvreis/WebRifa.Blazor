using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Enums;
using WebRifa.Blazor.Core.Interfaces;

namespace WebRifa.Blazor.Core.Factories;
public static class TicketStateFactory {
    public static ITicketState GetTicketState(TicketState state)
    {
        switch (state) {
            case TicketState.Valid:
                return new TicketValidState();
            case TicketState.Winner:
                return new TicketWinnerState();
            case TicketState.Loser:
                return new TicketLoserState();
            case TicketState.Canceled:
                return new TicketCanceledState();
            default:
                throw new ArgumentException("Choose a valid option.");
        }
    }
}