using WebRifa.Blazor.Core.Commands;
using WebRifa.Blazor.Core.Interfaces.Repositories;

namespace WebRifa.Blazor.Core.Services;
public class RaffleCoreService : IRaffleCoreService {
    private readonly ITicketRepository _ticketRepository;
    private readonly IReceiptRepository _receiptRepository;
    private readonly IBuyerRepository _buyerRepository;
    private readonly IRaffleRepository _raffleRepository;

    public RaffleCoreService(
        ITicketRepository ticketRepository,
        IReceiptRepository receiptRepository,
        IBuyerRepository buyerRepository,
        IRaffleRepository raffleRepository)
    {
        _ticketRepository = ticketRepository;
        _receiptRepository = receiptRepository;
        _buyerRepository = buyerRepository;
        _raffleRepository = raffleRepository;
    }

    public async Task BuyRaffleTicketsAsync(BuyRaffleTicketsCommand command, CancellationToken cancellationToken)
    {
        var freeNumbers = await GetFreeNumbersAsync(command.RaffleId, cancellationToken);
        bool areAllNumbersAvailable = command.NumbersToBuy.All(n => freeNumbers.Contains(n));

        if (!areAllNumbersAvailable) {
            throw new InvalidOperationException("Não foi possível registrar a compra, pois todos os números precisam estar disponíveis.");
        }

        Receipt receipt = new();

        foreach (var number in command.NumbersToBuy) {
            receipt.Tickets.Add(new(number, "temp", command.RaffleId)); // To-do: implement message that comes from command.
        }

        Buyer buyer = await _buyerRepository.GetAsync(command.BuyerId, cancellationToken);

        foreach (var ticket in receipt.Tickets) {
            receipt.AddBuyerTicketReceipt(new(buyer, ticket, receipt));
        }

        await _receiptRepository.AddAsync(receipt, cancellationToken);
    }

    public async Task<List<int>> GetFreeNumbersAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        var raffle = await _raffleRepository.GetAsync(raffleId, cancellationToken);
        var usedNumbers = raffle.Tickets?.Select(t => t.Number).ToList() ?? new();

        List<int> availableNumbers = new();

        for (int i = 1; i <= raffle.TotalNumberOfTickets; i++) {
            if (!usedNumbers.Contains(i)) {
                availableNumbers.Add(i);
            }
        }

        return availableNumbers;
    }
}