using AutoMapper;
using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Requests.Queries.Draw;
using WebRifa.Blazor.Core.Services;

namespace WebRifa.Blazor.Services;

public class DrawService(
    ILogger<DrawService> logger,
    IUnitOfWork unitOfWork,
    IRaffleCoreService raffleCoreService,
    IDrawRepository drawRepository,
    IMapper mapper) : IDrawService {

    public async Task<PaginatedList<DrawDto>> GetAllPaginatedAsync(DrawGetAllQuery query, CancellationToken cancellationToken)
    {
        try {
            logger.LogInformation($"{GetAllPaginatedAsync} executado.");
            var draws = await drawRepository.GetAllPaginatedAsync(query, cancellationToken);
            return mapper.Map<PaginatedList<DrawDto>>(draws);
        }
        catch (Exception) {

            throw;
        } 
    }

    public async Task<int> CarryOutTheDrawAsync(CarryOutTheDrawCommand command, CancellationToken cancellationToken)
    {
        try {
            logger.LogInformation("Sorteio executado para a rifa {RaflleId}", command.RaflleId);
            var drawnTicketNumber = await raffleCoreService.CarryOutTheDrawAsync(command.RaflleId, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return drawnTicketNumber;
        }
        catch (Exception) {

            throw;
        }
    }
}
