using WebRifa.Blazor.Core.Requests.Commands.Receipt;

namespace WebRifa.Blazor.Core.CoreServices;
public interface IReceiptCoreService {
    Task DeleteReceipt(ReceiptDeleteCommand command, CancellationToken cancellation);
}
