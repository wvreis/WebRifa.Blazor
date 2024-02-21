using WebRifa.Blazor.Core.Requests.Commands.Receipt;

namespace WebRifa.Blazor.Core.CoreServices;
public interface IReceiptCoreService {
    Task DeleteReceiptAsync(ReceiptDeleteCommand command, CancellationToken cancellation);
}
