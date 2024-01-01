using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Entities.Draw;
using WebRifa.Blazor.Core.Entities.Receipt;
using WebRifa.Blazor.Core.Entities.Ticket;

namespace WebRifa.Blazor.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Raffle>().HasKey(x => x.Id);

        builder.Entity<Raffle>()
            .Property(x => x.TicketPrice)
            .HasPrecision(4);

        builder.Entity<Buyer>().HasKey(x => x.Id);

        #region TICKET
        builder.Entity<Ticket>().HasKey(x => x.Id);

        builder.Entity<Ticket>()
            .Navigation(x => x.Buyer)
            .AutoInclude();

        builder.Entity<Ticket>()
            .Navigation(x => x.Raffle)
            .AutoInclude();

        builder.Entity<Ticket>()
            .Navigation(x => x.Receipt)
            .AutoInclude();

        builder.Entity<Ticket>()
            .HasOne(x => x.Buyer)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Ticket>()
            .HasOne(x => x.Raffle)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.RaffleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Ticket>()
            .HasOne(x => x.Draw)
            .WithMany()
            .HasForeignKey(x => x.DrawId) 
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Ticket>()
            .Ignore(x => x.State);
        #endregion

        #region RECEIPT
        builder.Entity<Receipt>().HasKey(x => x.Id);

        builder.Entity<Receipt>()
           .Navigation(x => x.Buyer)
           .AutoInclude();

        builder.Entity<Receipt>()
           .Navigation(x => x.Tickets)
           .AutoInclude();

        builder.Entity<Receipt>()
            .HasOne(x => x.Buyer)
            .WithMany(x => x.Receipts)
            .HasForeignKey(x => x.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Receipt>()
           .Ignore(x => x.State);
        #endregion

        #region DRAW
        builder.Entity<Draw>().HasKey(x => x.Id);

        builder.Entity<Draw>()
            .HasOne(x => x.RaffledTicket)
            .WithOne(x => x.Draw)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
    }

    public override DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Raffle> Raffles { get; set; }
    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<Draw> Draws { get; set; }
}