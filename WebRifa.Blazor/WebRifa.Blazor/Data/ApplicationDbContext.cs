using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Entities.DrawEntities;
using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Entities.TicketEntities;

namespace WebRifa.Blazor.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Raffle>().HasKey(x => x.Id);

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

        builder.Entity<Buyer>().HasKey(x => x.Id);

        #region TICKET
        builder.Entity<Ticket>().HasKey(x => x.Id);

        builder.Entity<Ticket>()
            .HasIndex(x => new { 
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

        builder.Entity<Receipt>()
           .Navigation(x => x.Tickets)
           .AutoInclude();

        builder.Entity<Receipt>()
           .Ignore(x => x.State);
        #endregion

        #region DRAW
        builder.Entity<Draw>().HasKey(x => x.Id);

        builder.Entity<Draw>()
            .HasOne(x => x.DrawnTicket)
            .WithOne(x => x.Draw)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region BUYERTICKETRECEIPT
        builder.Entity<BuyerTicketReceipt>().HasKey(x => x.Id);

        builder.Entity<BuyerTicketReceipt>()
            .HasOne(x => x.Buyer)
            .WithMany(x => x.BuyerTicketReceipts)
            .HasForeignKey(x => x.BuyerId);

        builder.Entity<BuyerTicketReceipt>()
            .HasOne(x => x.Ticket)
            .WithMany(x => x.BuyerTicketReceipt)
            .HasForeignKey(x => x.TicketId); 

        builder.Entity<BuyerTicketReceipt>()
            .HasOne(x => x.Receipt)
            .WithMany(x => x.BuyerTicketReceipt)
            .HasForeignKey(x => x.ReceiptId); 
        #endregion
    }

    public override DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Raffle> Raffles { get; set; }
    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<Draw> Draws { get; set; }
}
