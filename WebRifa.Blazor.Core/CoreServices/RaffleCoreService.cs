using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Requests.Commands;

namespace WebRifa.Blazor.Core.Services;
public class RaffleCoreService : IRaffleCoreService {
    private readonly IReceiptRepository _receiptRepository;
    private readonly IBuyerRepository _buyerRepository;
    private readonly IRaffleRepository _raffleRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IDrawRepository _drawRepository;
    private readonly IBuyerTicketReceiptRepository _buyerTicketReceiptRepository;

    public RaffleCoreService(
        IReceiptRepository receiptRepository,
        IBuyerRepository buyerRepository,
        IRaffleRepository raffleRepository,
        ITicketRepository ticketRepository,
        IDrawRepository drawRepository,
        IBuyerTicketReceiptRepository buyerTicketReceiptRepository)
    {
        _receiptRepository = receiptRepository;
        _buyerRepository = buyerRepository;
        _raffleRepository = raffleRepository;
        _ticketRepository = ticketRepository;
        _drawRepository = drawRepository;
        _buyerTicketReceiptRepository = buyerTicketReceiptRepository;
    }

    public async Task<Guid> BuyRaffleTicketsAsync(BuyRaffleTicketsCommand command, CancellationToken cancellationToken)
    {
        var freeNumbers = await GetFreeNumbersAsync(command.RaffleId!.Value, cancellationToken);
        bool areAllNumbersAvailable = command.NumbersToBuy.All(n => freeNumbers.Contains(n));

        if (!areAllNumbersAvailable) {
            throw new InvalidOperationException("Não foi possível registrar a compra, pois todos os números precisam estar disponíveis.");
        }

        Receipt receipt = new();
        Buyer buyer = await _buyerRepository.GetAsync(command.BuyerId!.Value, cancellationToken);

        foreach (var number in command.NumbersToBuy) {
            var ticket = new Ticket(number, command.Observations, command.RaffleId!.Value);
            receipt.AddTicket(ticket);
        }

        foreach (var ticket in receipt.Tickets) {
            receipt.AddBuyerTicketReceipt(new BuyerTicketReceipt(buyer, ticket, receipt));
        }

        return await _receiptRepository.AddAsync(receipt, cancellationToken);
    }

    public async Task<int> CarryOutTheDrawAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        Raffle raffle = await _raffleRepository.GetAsync(raffleId, cancellationToken);

        bool wasDrawDone = await _drawRepository.WasRaffleDrawDone(raffleId, cancellationToken);
        if (wasDrawDone) {
            throw new InvalidOperationException("O sorteio dessa Rifa já foi realizado.");
        }

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
        raffle.SetAsFinished();
        await _raffleRepository.UpdateAsync(raffle, cancellationToken);

        return drawnTicketNumber;
    }

    public async Task<HashSet<int>> GetFreeNumbersAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        int totalNumberOfTickets = await _raffleRepository.GetTotalNumberOfTicketsFromRaffleAsync(raffleId, cancellationToken);
        HashSet<int> usedNumbers = await GetUsedNumbersAsync(raffleId, cancellationToken);
        HashSet<int> availableNumbers = new HashSet<int>(Enumerable.Range(1, totalNumberOfTickets).Except(usedNumbers));

        return availableNumbers;
    }

    public async Task<HashSet<int>> GetUsedNumbersAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        var result = await _raffleRepository.GetUsedNumbersAsync(raffleId, cancellationToken);
        return result;
    }

    public async Task DeleteRaffleAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        Raffle? raffle = await _raffleRepository.GetAsync(raffleId, cancellationToken);
        if (raffle is null) {
            throw new NullReferenceException();
        }

        List<Receipt> receipts = await _receiptRepository.GetReceiptsFromRaffleAsync(raffleId, cancellationToken);
        List<BuyerTicketReceipt> buyerTicketReceipts = receipts.SelectMany(r => r.BuyerTicketReceipts).ToList();

        if (raffle.Tickets is not null && raffle.Tickets!.Any()) {
            await _ticketRepository.DeleteRangeAsync(raffle.Tickets, cancellationToken);
        }

        if (receipts.Any()) {
            await _receiptRepository.DeleteRangeAsync(receipts, cancellationToken);
        }

        if (buyerTicketReceipts.Any()) {
            await _buyerTicketReceiptRepository.DeleteRangeAsync(buyerTicketReceipts, cancellationToken);
        }

        await _raffleRepository.DeleteAsync(raffle, cancellationToken);
    }
}