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
        
        builder.Entity<Buyer>().HasKey(x => x.Id);

        #region TICKET
        builder.Entity<Ticket>().HasKey(x => x.Id);
        
        builder.Entity<Ticket>()
            .HasOne(x => x.Buyer)
            .WithMany(x => x.Tickets)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Ticket>()
            .HasOne(x => x.Raffle)
            .WithMany(x => x.Tickets)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region RECEIPT
        builder.Entity<Receipt>().HasKey(x => x.Id);

        builder.Entity<Receipt>()
            .HasOne(x => x.Buyer)
            .WithMany(x => x.Receipts)
            .OnDelete(DeleteBehavior.Restrict);
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
