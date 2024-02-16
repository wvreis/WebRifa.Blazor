using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Requests.Commands.Receipt;

namespace WebRifa.Blazor.Core.CoreServices;
public class ReceiptCoreService(IReceiptRepository receiptRepository, ITicketRepository ticketRepository) : IReceiptCoreService {
    private readonly IReceiptRepository _receiptRepository = receiptRepository;
    private readonly ITicketRepository _ticketRepository = ticketRepository;

    public async Task DeleteReceipt(ReceiptDeleteCommand command, CancellationToken cancellation)
    {
        Receipt receipt = await _receiptRepository.GetAsync(command.ReceiptId, cancellation);

        await _receiptRepository.DeleteAsync(receipt, cancellation);
        await _ticketRepository.DeleteRangeAsync(receipt.Tickets, cancellation);
    }
}
