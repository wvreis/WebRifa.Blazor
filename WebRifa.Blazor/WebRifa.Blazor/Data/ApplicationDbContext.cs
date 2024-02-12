using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Entities.DrawEntities;
using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Entities.TicketEntities;
using WebRifa.Blazor.Services.UserServices;

namespace WebRifa.Blazor.Data;
public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,    
    ICustomUserIdProvider userIdProvider) : IdentityDbContext<ApplicationUser>(options) {
    
    private readonly ICustomUserIdProvider _userIdProvider = userIdProvider;    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);               

        #region RAFFLE
        builder.Entity<Raffle>().HasKey(x => x.Id);        

        builder.Entity<Raffle>().HasQueryFilter(x => !x.IsDeleted);

        builder.Entity<Raffle>()
            .Property(x => x.TicketPrice)
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2);

        builder.Entity<Raffle>()
            .HasMany(x => x.Tickets)
            .WithOne(x => x.Raffle)
            .HasForeignKey(x => x.RaffleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Raffle>()
            .Navigation(x => x.Tickets)
            .AutoInclude();
        #endregion

        #region BUYER
        builder.Entity<Buyer>().HasKey(b => b.Id);

        builder.Entity<Buyer>().HasIndex(b => b.CreatedBy);        
        #endregion

        #region TICKET
        builder.Entity<Ticket>().HasKey(x => x.Id);

        builder.Entity<Ticket>().HasQueryFilter(x => !x.IsDeleted);

        builder.Entity<Ticket>()
            .HasIndex(x => new
            {
                x.RaffleId,
                x.Number,
                x.IsDeleted
            })
            .IsUnique();

        builder.Entity<Ticket>()
            .Navigation(x => x.BuyerTicketReceipt)
            .AutoInclude();

        builder.Entity<Ticket>()
            .Navigation(x => x.Receipt)
            .AutoInclude();

        builder.Entity<Ticket>()
            .HasOne(x => x.Raffle)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.RaffleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Ticket>()
            .HasOne(x => x.Draw)
            .WithOne(x => x.DrawnTicket)
            .HasForeignKey<Ticket>(x => x.DrawId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Ticket>()
            .Ignore(x => x.State);
        #endregion

        #region RECEIPT
        builder.Entity<Receipt>().HasKey(x => x.Id);

        builder.Entity<Receipt>().HasQueryFilter(x => !x.IsDeleted);

        builder.Entity<Receipt>()
           .Navigation(x => x.Tickets)
           .AutoInclude();

        builder.Entity<Receipt>()
           .Ignore(x => x.State);
        #endregion

        #region DRAW
        builder.Entity<Draw>().HasKey(x => x.Id);

        builder.Entity<Draw>().HasQueryFilter(x => !x.IsDeleted);

        builder.Entity<Draw>()
            .HasOne(x => x.DrawnTicket)
            .WithOne(x => x.Draw)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region BUYERTICKETRECEIPT
        builder.Entity<BuyerTicketReceipt>().HasKey(x => x.Id);

        builder.Entity<BuyerTicketReceipt>().HasQueryFilter(x => !x.IsDeleted);

        builder.Entity<BuyerTicketReceipt>()
            .HasIndex(x => new
            {
                x.BuyerId,
                x.TicketId,
                x.ReceiptId,
                x.IsDeleted
            })
            .IsUnique();

        builder.Entity<BuyerTicketReceipt>()
            .HasOne(x => x.Buyer)
            .WithMany(x => x.BuyerTicketReceipts)
            .HasForeignKey(x => x.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<BuyerTicketReceipt>()
            .HasOne(x => x.Ticket)
            .WithMany(x => x.BuyerTicketReceipt)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<BuyerTicketReceipt>()
            .HasOne(x => x.Receipt)
            .WithMany(x => x.BuyerTicketReceipt)
            .HasForeignKey(x => x.ReceiptId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        this.SetGlobalQueryForUser(builder);
    }

    private void SetGlobalQueryForUser(ModelBuilder builder)
    {
        builder.Entity<Raffle>().HasQueryFilter(b => b.CreatedBy == GetCurrentUserId());
        builder.Entity<Buyer>().HasQueryFilter(b => b.CreatedBy == GetCurrentUserId());
        builder.Entity<Ticket>().HasQueryFilter(b => b.CreatedBy == GetCurrentUserId());
        builder.Entity<Receipt>().HasQueryFilter(b => b.CreatedBy == GetCurrentUserId());
        builder.Entity<Draw>().HasQueryFilter(b => b.CreatedBy == GetCurrentUserId());
        builder.Entity<BuyerTicketReceipt>().HasQueryFilter(b => b.CreatedBy == GetCurrentUserId());
    }

    private Guid GetCurrentUserId()
    {        
        return _userIdProvider.GetUserIdAsync().GetAwaiter().GetResult();
    }

    public override DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Raffle> Raffles { get; set; }
    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<Draw> Draws { get; set; }
}
