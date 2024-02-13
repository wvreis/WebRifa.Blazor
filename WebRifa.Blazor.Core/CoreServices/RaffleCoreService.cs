using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Requests.Commands;

namespace WebRifa.Blazor.Core.Services;
public class RaffleCoreService : IRaffleCoreService {
    private readonly IReceiptRepository _receiptRepository;
    private readonly IBuyerRepository _buyerRepository;
    private readonly IRaffleRepository _raffleRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IDrawRepository _drawRepository;

    public RaffleCoreService(
        IReceiptRepository receiptRepository,
        IBuyerRepository buyerRepository,
        IRaffleRepository raffleRepository,
        ITicketRepository ticketRepository,
        IDrawRepository drawRepository)
    {
        _receiptRepository = receiptRepository;
        _buyerRepository = buyerRepository;
        _raffleRepository = raffleRepository;
        _ticketRepository = ticketRepository;
        _drawRepository = drawRepository;
    }

    public async Task BuyRaffleTicketsAsync(BuyRaffleTicketsCommand command, CancellationToken cancellationToken)
    {
        var freeNumbers = await GetFreeNumbersAsync(command.RaffleId, cancellationToken);
        bool areAllNumbersAvailable = command.NumbersToBuy.All(n => freeNumbers.Contains(n));

        if (!areAllNumbersAvailable) {
            throw new InvalidOperationException("Não foi possível registrar a compra, pois todos os números precisam estar disponíveis.");
        }

        Receipt receipt = new();
        Buyer buyer = await _buyerRepository.GetAsync(command.BuyerId, cancellationToken);

        foreach (var number in command.NumbersToBuy) {
            var ticket = new Ticket(number, command.Observations, command.RaffleId);
            receipt.AddTicket(ticket);
        }

        foreach (var ticket in receipt.Tickets) {
            receipt.AddBuyerTicketReceipt(new BuyerTicketReceipt(buyer, ticket, receipt));
        }

        await _receiptRepository.AddAsync(receipt, cancellationToken);
    }

    public async Task<int> CarryOutTheDrawAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        Raffle raffle = await _raffleRepository.GetAsync(raffleId, cancellationToken);

        var usedNumbers = await GetUsedNumbersAsync(raffleId, cancellationToken);
        var randomIndex = new Random().Next(0, usedNumbers.Count);

        var drawnTicketNumber = usedNumbers.ToList()[randomIndex];
        var drawnTicket = raffle?.Tickets?.FirstOrDefault(t => t.Number.Equals(drawnTicketNumber));

        raffle?.Tickets?.ForEach(async ticket => {
            if (ticket.Number == drawnTicketNumber) {
                ticket.MarkAsWinner();
            }
            else {
                ticket.MarkAsLoser();
            }

            await _ticketRepository.UpdateAsync(ticket, cancellationToken);
        });

        if (raffle is null || drawnTicket is null) {
            throw new ArgumentNullException();
        }

        Draw draw = new(drawnTicket, raffle);

        await _drawRepository.AddAsync(draw, cancellationToken);

        return drawnTicketNumber;
    }

    public async Task<HashSet<int>> GetFreeNumbersAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        Raffle raffle = await _raffleRepository.GetAsync(raffleId, cancellationToken);

        HashSet<int> usedNumbers = await GetUsedNumbersAsync(raffleId, cancellationToken);

        HashSet<int> availableNumbers = new HashSet<int>(Enumerable.Range(1, raffle.TotalNumberOfTickets).Except(usedNumbers));

        return availableNumbers;
    }

    public async Task<HashSet<int>> GetUsedNumbersAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        Raffle raffle = await _raffleRepository.GetAsync(raffleId, cancellationToken);

        HashSet<int> usedNumbers = raffle.Tickets?.Select(t => t.Number).ToHashSet() ?? new();

        return usedNumbers;
    }

    public async Task DeleteRaffleAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        Raffle raffle = await _raffleRepository.GetAsync(raffleId, cancellationToken);
        List<Receipt> receipts = await _receiptRepository.GetReceiptsFromRaffleAsync(raffleId, cancellationToken);

        await _ticketRepository.DeleteRangeAsync(raffle.Tickets ?? new(), cancellationToken);
        await _receiptRepository.DeleteRangeAsync(receipts, cancellationToken);
        await _raffleRepository.DeleteAsync(raffle, cancellationToken);
    }
}