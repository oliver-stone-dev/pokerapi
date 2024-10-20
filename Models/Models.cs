﻿using Microsoft.AspNetCore.Identity;

namespace PokerAppAPI.Models;

//Create custom user account table derived from EF auth indentity user
public class Account : IdentityUser
{
    public int Chips { get; set; }
    //public ICollection<Player> Players { get; } = new List<Player>();
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

  //  public ICollection<Bet> Bets { get; } = new List<Bet>();

  //  public ICollection<Event> Actions { get; } = new List<Event>();
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

  //  public ICollection<Event> Actions { get; } = new List<Event>();

   // public ICollection<Bet> Bets { get; } = new List<Bet>();

   // public ICollection<Pot> Pots { get; } = new List<Pot>();
}

/*public class Event
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int PlayerId { get; set; }
    public int Type { get; set; }
    public int TimeStamp { get; set; }
}*/

/*public class Bet
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int PlayerId { get; set; }
    public int PotId { get; set; }
    public int Amount { get; set; }
    public int TimeStamp { get; set; }
}*/

/*public class Pot
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int Amount { get; set; }
    public int[]? PlayersIds { get; set; } = null;
}*/