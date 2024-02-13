namespace WebRifa.Blazor.Core.Interfaces.Repositories;

public interface IReceiptRepository : IBaseRepository<Receipt> 
{
    Task<List<Receipt>> GetReceiptsFromRaffleAsync(Guid raffleId, CancellationToken cancellationToken);
}
