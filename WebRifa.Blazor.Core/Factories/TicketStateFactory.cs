using WebRifa.Blazor.Core.Enums;
using WebRifa.Blazor.Core.Interfaces;

namespace WebRifa.Blazor.Core.Factories;
public static class TicketStateFactory {
    public static ITicketState GetTicketState(this TicketStates state)
    {
        switch (state) {
            case TicketStates.Valid:
                return new TicketValidState();
            case TicketStates.Winner:
                return new TicketWinnerState();
            case TicketStates.Loser:
                return new TicketLoserState();
            case TicketStates.Canceled:
                return new TicketCanceledState();
            default:
                throw new ArgumentException("Choose a valid option.");
        }
    }
}