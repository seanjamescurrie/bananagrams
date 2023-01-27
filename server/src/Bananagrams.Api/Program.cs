using Bananagrams.Api.Filters;
using Bananagrams.Api.Profiles;
using Bananagrams.Dal.Contexts;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Bananagrams.Service.Interfaces;
using Bananagrams.Service.Profiles;
using Bananagrams.Service.Services;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x => { x.Filters.Add<ExceptionFilter>(); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBananagramsDatabase, BananagramsDatabase>(_ =>
    new BananagramsDatabase("Server=localhost,5432;Database=postgres;User Id=user;Password=password;"))
                .AddScoped<IUserService, UserService>()
                .AddScoped<IGameService, GameService>()
                .AddScoped<IWordService, WordService>()
                .AddScoped<IGameAnagramService, GameAnagramService>()
                .AddScoped<IGameUserService, GameUserService>()
                .AddScoped<IGameUserGameAnagramService, GameUserGameAnagramService>();
builder.Services.AddAutoMapper(config => config.AllowNullCollections = true, typeof(Program).Assembly, typeof(GameProfile).Assembly);
// builder.Services.AddAutoMapper(config => config.AllowNullCollections = true, typeof(Program).Assembly, typeof(UserProfile).Assembly);
// builder.Services.AddAutoMapper(config => config.AllowNullCollections = true, typeof(Program).Assembly, typeof(UserDtoProfile).Assembly);

builder.Services.AddFluentValidation(s =>
    s.RegisterValidatorsFromAssemblyContaining<Program>()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { };