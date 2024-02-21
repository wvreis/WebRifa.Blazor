using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Requests.Commands.Receipt;

namespace WebRifa.Blazor.Core.CoreServices;
public class ReceiptCoreService(IReceiptRepository receiptRepository, ITicketRepository ticketRepository) : IReceiptCoreService {
    private readonly IReceiptRepository _receiptRepository = receiptRepository;
    private readonly ITicketRepository _ticketRepository = ticketRepository;

    public async Task DeleteReceiptAsync(ReceiptDeleteCommand command, CancellationToken cancellation)
    {
        Receipt receipt = await _receiptRepository.GetAsync(command.ReceiptId, cancellation);
        if (receipt == null) {
            throw new NullReferenceException();
        }

        await _receiptRepository.DeleteAsync(receipt, cancellation);

        if (receipt.Tickets is not null) {
            await _ticketRepository.DeleteRangeAsync(receipt.Tickets, cancellation);
        }
    }
}
