﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class ReceiptRepository(
    ApplicationDbContext context) : BaseRepository<Receipt>(context), IReceiptRepository {
    public override async Task<List<Receipt>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Receipts
            .IgnoreAutoIncludes()
            .Include(x => x.BuyerTicketReceipts)
                .ThenInclude(x => x.Buyer)
            .Include(x => x.Tickets)
                .ThenInclude(x => x.Raffle)
            .Where(receipt => !receipt.IsDeleted)
            .ToListAsync();
    }

    public async Task<PaginatedList<Receipt>> GetAllPaginatedAsync(ReceiptGetAllQuery query, CancellationToken cancellationToken)
    {
        List<Receipt> receipts = await context.Receipts
            .IgnoreAutoIncludes()
            .AsSingleQuery()
            .Include(x => x.BuyerTicketReceipts)
                .ThenInclude(x => x.Buyer)
            .Include(x => x.Tickets)
                .ThenInclude(x => x.Raffle)
            .Where(receipt => !receipt.IsDeleted)
            .Skip((query.CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        int totalCountItems = await context.Receipts
            .Where(receipt => !receipt.IsDeleted)            
            .CountAsync();

        int totalPages = (int)Math.Ceiling(
            Convert.ToDecimal(totalCountItems) /
            Convert.ToDecimal(PageSize));

        var paginatedList = new PaginatedList<Receipt>(query.CurrentPage, totalPages, PageSize, receipts);
        return paginatedList;
    }

    public override async Task<Receipt> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await context.Receipts
            .IgnoreAutoIncludes()            
            .Include(x => x.BuyerTicketReceipts)
                .ThenInclude(x => x.Buyer)
            .Include(x => x.Tickets)
                .ThenInclude(x => x.Raffle)
            .Where(receipt => receipt.Id == id)
            .Where(receipt =>  !receipt.IsDeleted)
            .FirstOrDefaultAsync();

        if (result is null) {
            throw new ArgumentNullException();
        }

        return result;
    }

    public async Task<Receipt> GetPublicAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await context.Receipts
            .IgnoreQueryFilters()
            .IgnoreAutoIncludes()
            .Include(x => x.BuyerTicketReceipts)
                .ThenInclude(x => x.Buyer)
            .Include(x => x.Tickets)
                .ThenInclude(x => x.Raffle)
            .Where(receipt => receipt.Id == id)
            .Where(receipt => !receipt.IsDeleted)
            .FirstOrDefaultAsync();

        if (result is null) {
            throw new ArgumentNullException();
        }

        return result;
    }

    public async Task<List<Receipt>> GetFilteredReceiptsAsync(ReceiptsGetFilteredQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<Receipt, bool>> buyerIdFilter = receipt =>
            query.BuyerId.HasValue ?
            receipt.BuyerTicketReceipts!.FirstOrDefault()!.BuyerId == query.BuyerId : 
            true;

        Expression<Func<Receipt, bool>> raffleIdFilter = receipt =>
            query.RaffleId.HasValue ?
            receipt.BuyerTicketReceipts!.FirstOrDefault()!.Ticket!.RaffleId == query.RaffleId :
            true;
        
        var result = await context.Receipts
            .Include(x => x.BuyerTicketReceipts)
                .ThenInclude(x => x.Buyer)
            .Include(x => x.Tickets)
                .ThenInclude(x => x.Raffle)
            .Where(buyerIdFilter)
            .Where(raffleIdFilter)
            .Where(receipt => !receipt.IsDeleted)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<List<Receipt>> GetReceiptsFromRaffleAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        Expression<Func<Receipt, bool>> fromRaffleId = receipt =>
            receipt.BuyerTicketReceipts.Any(btr => btr.Ticket!.RaffleId == raffleId);

        var result = await context.Receipts
            .Include(x => x.BuyerTicketReceipts)
                .ThenInclude(x => x.Buyer)
            .Include(x => x.Tickets)
                .ThenInclude(x => x.Raffle)
            .Where(fromRaffleId)
            .Where(receipt => !receipt.IsDeleted)
            .ToListAsync();

        return result;
    }
}
