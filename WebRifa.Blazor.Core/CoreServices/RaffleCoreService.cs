using WebRifa.Blazor.Core.Interfaces.Repositories;

namespace WebRifa.Blazor.Core.Services;
public class RaffleCoreService 
{
    private readonly IRaffleRepository _raffleRepository;

    public RaffleCoreService(IRaffleRepository raffleRepository)
    {
        _raffleRepository = raffleRepository;
    }

    //public async Task
}