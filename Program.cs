using Microsoft.EntityFrameworkCore;

namespace PokerAppAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Add sqlite dependancy
        builder.Services.AddSqlite<PokerDb>
        (
            builder.Configuration.GetConnectionString("PokerDb") ?? "Data Source = PokerDb.db"
        );

        var app = builder.Build();

        //General purpose endpoints
        app.MapGet("/games", () => "List of games.");

        //Game specific endpoints
        app.MapGet("/games/{gameId}/state", (int gameId) => $"{gameId} state");
        app.MapGet("/games/{gameId}/players", (int gameId) => $"{gameId} players");

        app.Run();
    }
}

//Account item for storing user account data
public class Account
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int Chips { get; set; }
    public ICollection<Player> Players { get; } = new List<Player>();
}

//Player item for storing player specific game state. Foreign keys to game and account.
public class Player
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int GameId { get; set; }
    public int Chips { get; set; }
    public string? Cards { get; set; }
    public bool Folded { get; set; }
    public bool AllIn { get; set; }

    public ICollection<Bet> Bets { get; } = new List<Bet>();

    public ICollection<Action> Actions { get; } = new List<Action>();
}

//Game item that stores game state.
public class Game
{
    public int Id { get; set; }
    public int PlayerCount { get; set; }
    public bool Open { get; set; }
    public int BigBlind { get; set; }
    public int SmallBlind { get; set; }
    public int Pot { get; set; }
    public int CurrentBet { get; set; }
    public string? Deck { get; set; }
    public string? BurnCards { get; set; }
    public string? CommunityCards { get; set; }
    public int DealerPosition { get; set; }
    public int MovePosition { get; set; }
    public int BetStage { get; set; }
    public ICollection<Player> Players { get; } = new List<Player>();

    public ICollection<Action> Actions { get; } = new List<Action>();

    public ICollection<Bet> Bets { get; } = new List<Bet>();

    public ICollection<Pot> Pots { get; } = new List<Pot>();
}

public class Action
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int PlayerId { get; set; }
    public int Type { get; set; }
    public int TimeStamp { get; set; }
}

public class Bet
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int PlayerId { get; set; }
    public int PotId { get; set; }
    public int Amount { get; set; }
    public int TimeStamp { get; set; }
}

public class Pot
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int Amount { get; set; }
    public int[]? PlayersIds { get; set; } = null;
}

public class PokerDb : DbContext
{
    public PokerDb(DbContextOptions options) : base(options) { }
    public DbSet<Account> Accounts { get; set; } = null;
    public DbSet<Player> Players { get; set; } = null;
    public DbSet<Game> Games { get; set; } = null;
    public DbSet<Action> Actions { get; set; } = null;
    public DbSet<Bet> Bets { get; set; } = null;
    public DbSet<Pot> Pots { get; set; } = null;
}

