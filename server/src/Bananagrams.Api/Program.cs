using Bananagrams.Api;
using Bananagrams.Api.Authentication;
using Bananagrams.Api.Filters;
using Bananagrams.Api.Hubs;
using Bananagrams.Api.Hubs.Clients;
using Bananagrams.Dal.Contexts;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Service.HttpClients;
using Bananagrams.Service.Interfaces;
using Bananagrams.Service.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using AuthenticationService = Bananagrams.Service.Services.AuthenticationService;
using GameProfile = Bananagrams.Service.Profiles.GameProfile;
using IAuthenticationService = Bananagrams.Service.Interfaces.IAuthenticationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x => { x.Filters.Add<ExceptionFilter>(); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "You api title", Version = "v1" });
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme()
        {
            In = ParameterLocation.Header,
            Description = "Please enter into field the word 'Bearer' following by space and JWT",
            Name = "Authorization", Type = SecuritySchemeType.ApiKey
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        { { new OpenApiSecurityScheme()
        {
            Reference = new OpenApiReference()
            {
                Type = ReferenceType.SecurityScheme, Id = "Bearer"
            },
        }, new List<string>() } });
});
builder.Services.AddScoped<IBananagramsDatabase, BananagramsDatabase>(_ =>
        new BananagramsDatabase(EnvironmentVariables.DbConnectionString))
    .AddScoped<IUserService, UserService>()
    .AddScoped<IGameService, GameService>()
    .AddScoped<IGameTypeService, GameTypeService>()
    .AddScoped<IWordService, WordService>()
    .AddScoped<IAuthenticationService, AuthenticationService>()
    .AddScoped<ITropicalFruitApiService, TropicalFruitApiService>()
    .AddScoped<IAuthorizedAccountProvider, AuthorizedAccountProvider>();
builder.Services.AddAutoMapper(config => config.AllowNullCollections = true, typeof(Program).Assembly,
    typeof(GameProfile).Assembly);

builder.Services.AddHealthChecks();

builder.Services.AddHttpClient();

builder.Services.AddFluentValidation(s =>
    s.RegisterValidatorsFromAssemblyContaining<Program>()
);

builder.Services.AddAuthentication(string.Empty)
    .AddScheme<AuthenticationSchemeOptions, AccessAuthenticationFilter>(string.Empty, options => { });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(string.Empty, policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddSignalR(cfg => cfg.EnableDetailedErrors = true);
// builder.Services.AddTransient<INotificationClient, NotificationHub>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.MapHealthChecks("/health");

app.UseCors(
    o => o
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true)
        .AllowCredentials()
        // .AllowAnyOrigin()
        // .DisallowCredentials()
        // .WithOrigins("http://localhost:3000")
);

app.MapHub<NotificationHub>("/hub");
app.MapHub<GameHub>("/hub/game");

app.Run();

public partial class Program
{
};