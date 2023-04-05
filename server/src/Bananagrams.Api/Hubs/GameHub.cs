using System.Collections.Concurrent;
using System.Security.Claims;
using Bananagrams.Api.Hubs.Clients;
using Bananagrams.Api.Authentication;
using Bananagrams.Api.ViewModels.GameUserGameAnagrams;
using Bananagrams.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Bananagrams.Api.Hubs;

public class GameHub : Hub
{
    private readonly IAuthorizedAccountProvider _authorize;
    private readonly IGameService _gameService;
    private readonly IUserService _userService;
    private static readonly ConcurrentDictionary<string, List<string>> _groupConnections = new();

    public GameHub(IAuthorizedAccountProvider authorize, IGameService gameService, IUserService userService) =>
    (_authorize, _gameService, _userService) = (authorize, gameService, userService);

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        
        var httpContext = Context.GetHttpContext();
        var userId = httpContext?.Request.Query["userId"];
        var gameId = httpContext?.Request.Query["gameId"];
        
        var user = await _userService.Get(int.Parse(userId));
        var game = await _gameService.Get(int.Parse(gameId));
        
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        
        await Clients.Group(gameId).SendAsync("UserJoinedLobby", user.Username);
        
        // grouConnection convention = userId--connectionId
        if (_groupConnections.TryGetValue(gameId, out var value))
        {
            value.Add($"{userId}--{Context.ConnectionId}");
        }
        else
        {
            _groupConnections.GetOrAdd(gameId, new List<string> { $"{userId}--{Context.ConnectionId}" });
        }

        _groupConnections.TryGetValue(gameId, out var users);
        var gameUserIds = game.GameUsers.Select(x => x.UserId.ToString()).ToList();
        var groupUserIds = new List<string>();
        foreach (var userInGroupConnections in users)
        {
            var userArray = userInGroupConnections.Split("--");
            groupUserIds.Add(userArray[0]);
        }
        
        // check if all game users presents
        if (Enumerable.SequenceEqual(gameUserIds.OrderBy(x => x), groupUserIds.OrderBy(x => x)))
        {
            // if true emit start game message
              await Clients.Group(gameId).SendAsync("StartGame", gameId);
        }
        
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // remove people on disconnect
        var connectionId = Context.ConnectionId;

       var games = _groupConnections.Keys;

        foreach (var game in games)
        {
            _groupConnections.TryGetValue(game, out var values);
            foreach (var value in values)
            {
                var split = value.Split("--");
                if (split[1] == connectionId)
                {
                    values.Remove(value);
                    break;
                }
                if (values.Count < 1) break;
            }
        }
        
        base.OnDisconnectedAsync(exception);
    }

    // public async Task SendUpdate(GameUserGameAnagramResponseViewModel GameUserGameAnagramResponse)
    // {
    //     await Clients.Group(GameUserGameAnagramResponse.GameId.ToString()).SendAsync("SendUpdate", GameUserGameAnagramResponse);
    // }
}