using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PokerAppAPI.Models;

namespace PokerAppAPI.Resources;

public class PokerDb : IdentityDbContext
{
    public PokerDb(DbContextOptions<PokerDb> options) : base(options) { }
    public DbSet<Account> Accounts { get; set; } = null;
    public DbSet<Player> Players { get; set; } = null;
    public DbSet<Game> Games { get; set; } = null;
    //public DbSet<Event> Events { get; set; } = null;
    //public DbSet<Bet> Bets { get; set; } = null;
   // public DbSet<Pot> Pots { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder builder)
    {

        //Restrict cascading deletes on player to bets relation. 

        base.OnModelCreating(builder);

/*        builder.Entity<Bet>()
            .HasOne<Player>() // bet has one player
            .WithMany() // player has many bets
            .HasForeignKey(o => o.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);*/
 
    }
}