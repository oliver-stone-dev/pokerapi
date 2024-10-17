using Microsoft.EntityFrameworkCore;
using PokerAppAPI.Resources;
using PokerAppAPI.Controllers;
using PokerAppAPI.Models;
using PokerAppAPI.Services;

namespace PokerAppAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        //Add sqlite dependancy
        builder.Services.AddSqlite<PokerDb>
        (
            builder.Configuration.GetConnectionString("PokerDb") ?? "Data Source = PokerDb.db"
        );

        builder.Services.AddScoped<IAccountService, AccountService>();

        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}

