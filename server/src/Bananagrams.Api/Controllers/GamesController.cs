using System.Net;
using AutoMapper;
using Bananagrams.Api.Hubs;
using Bananagrams.Api.Hubs.Clients;
using Bananagrams.Api.ViewModels.Games;
using Bananagrams.Api.ViewModels.GameTypes;
using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Bananagrams.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : BananagramsBaseController
{
    private readonly IGameService _gameService;
    private readonly IGameTypeService _gameTypeService;
    private readonly IMapper _mapper;
    // private readonly IHubContext<NotificationHub> _notificationHub;
    private readonly IHubContext<NotificationHub> _notificationHub;

    public GamesController(IGameService gameService, IGameTypeService gameTypeService, IMapper mapper, IHubContext<NotificationHub> notificationHub) =>
        (_gameService, _gameTypeService, _mapper, _notificationHub) = (gameService, gameTypeService, mapper, notificationHub);

    [HttpGet]
    public async Task<ActionResult<IList<GameViewModel>>> GetAll([FromQuery] string? title)
    {
        var games = await _gameService.GetAll(title);

        var viewModel = _mapper.Map<List<GameViewModel>>(games);

        return OkOrNoContent(viewModel);
    }
    
    [HttpGet("types/")]
    public async Task<ActionResult<IList<GameTypeViewModel>>> GetAllGameTypes()
    {
        var gameTypes = await _gameTypeService.GetAll();

        var viewModel = _mapper.Map<List<GameTypeViewModel>>(gameTypes);

        return OkOrNoContent(viewModel);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameDetailViewModel>> Get(int id)
    {
        var game = await _gameService.Get(id);

        var viewModel = _mapper.Map<GameDetailViewModel>(game);

        return OkOrNoNotFound(viewModel);
    }

    [HttpGet("daily")]
    public async Task<ActionResult<GameDetailViewModel>> GetDaily()
    {
        var game = await _gameService.GetDaily();

        var viewModel = _mapper.Map<GameDetailViewModel>(game);

        return OkOrNoNotFound(viewModel);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] CreateGameViewModel gameDetails)
    {
        var id = await _gameService.Create(_mapper.Map<CreateGameDto>(gameDetails));
        await _notificationHub.Clients.All.SendAsync("NotificationCount");
        
        var response = new GameCreatedViewModel
        {
            Id = id
        };
        
        return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
    }

    [HttpPut("{id}/Attempt/{anagramId}")]
    public async Task<ActionResult<bool>> UpdateGameUserGameAnagramAttempt(int id, int anagramId, [FromBody] UpdateGameViewModel gameDetails)
    {
        var updateGame = _mapper.Map<UpdateGameUserGameAnagramDto>(gameDetails);
        
        var isSolved = await _gameService.UpdateGameAnagramForUser(id, anagramId, updateGame);
        
        return OkOrNoNotFound(isSolved);
    }
}