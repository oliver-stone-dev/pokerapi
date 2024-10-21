using Microsoft.EntityFrameworkCore;
using PokerAppAPI.Resources;
using PokerAppAPI.Controllers;
using PokerAppAPI.Models;
using PokerAppAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace PokerAppAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        //Adds auth token input
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        }
        );

        //Add sqlserver dependancy and set to use connection string from app settings
        builder.Services.AddDbContext<PokerDb>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddAuthorization();

        builder.Services.AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<PokerDb>();

        builder.Services.AddScoped<IAccountService, AccountService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.MapIdentityApi<IdentityUser>();

        app.Run();
    }
}

