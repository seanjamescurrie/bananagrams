using Bananagrams.Api.Authentication;
using Bananagrams.Api.Filters;
using Bananagrams.Dal.Contexts;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Service.Interfaces;
using Bananagrams.Service.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AuthenticationService = Bananagrams.Service.Services.AuthenticationService;
using GameProfile = Bananagrams.Service.Profiles.GameProfile;
using IAuthenticationService = Bananagrams.Service.Interfaces.IAuthenticationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x => { x.Filters.Add<ExceptionFilter>(); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBananagramsDatabase, BananagramsDatabase>(_ =>
        new BananagramsDatabase(EnvironmentVariables.DbConnectionString))
    .AddScoped<IUserService, UserService>()
    .AddScoped<IGameService, GameService>()
    .AddScoped<IWordService, WordService>()
    .AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddAutoMapper(config => config.AllowNullCollections = true, typeof(Program).Assembly, typeof(GameProfile).Assembly);

builder.Services.AddFluentValidation(s =>
    s.RegisterValidatorsFromAssemblyContaining<Program>()
);

builder.Services.AddAuthentication(string.Empty).AddScheme<AuthenticationSchemeOptions, AccessAuthenticationFilter>(string.Empty, options => {});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(string.Empty, policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();

public partial class Program { };