using Microsoft.EntityFrameworkCore;
using PokerAppAPI.Models;

namespace PokerAppAPI.Resources;

public class PokerDb : DbContext
{
    public PokerDb(DbContextOptions options) : base(options) { }
    public DbSet<Account> Accounts { get; set; } = null;
    public DbSet<Player> Players { get; set; } = null;
    public DbSet<Game> Games { get; set; } = null;
    public DbSet<PokerAppAPI.Models.Action> Actions { get; set; } = null;
    public DbSet<Bet> Bets { get; set; } = null;
    public DbSet<Pot> Pots { get; set; } = null;
}