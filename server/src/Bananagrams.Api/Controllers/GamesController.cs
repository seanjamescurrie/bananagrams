using System.Net;
using AutoMapper;
using Bananagrams.Api.ViewModels.Games;
using Bananagrams.Service.Dtos;
using Bananagrams.Service.Dtos.GameUserGameAnagrams;
using Bananagrams.Service.Exceptions;
using Bananagrams.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : BananagramsBaseController
{
    private readonly IGameService _gameService;
    private readonly IMapper _mapper;

    public GamesController(IGameService gameService, IMapper mapper) =>
        (_gameService, _mapper) = (gameService, mapper);

    [HttpGet]
    public async Task<ActionResult<IList<GameViewModel>>> GetAll([FromQuery] string? title)
    {
        var games = await _gameService.GetAll(title);

        var viewModel = _mapper.Map<List<GameViewModel>>(games);

        return OkOrNoContent(viewModel);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameDetailViewModel>> Get(int id)
    {
        var game = await _gameService.Get(id);

        var viewModel = _mapper.Map<GameDetailViewModel>(game);

        return OkOrNoNotFound(viewModel);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateGameViewModel gameDetails)
    {
        await _gameService.Create(_mapper.Map<CreateGameDto>(gameDetails));

        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut("{id}/Attempt/{anagramId}")]
    public async Task<ActionResult> UpdateGameUserGameAnagramAttempt(int id, int anagramId, [FromBody] UpdateGameViewModel gameDetails)
    {
        var updateGame = _mapper.Map<UpdateGameUserGameAnagramDto>(gameDetails);
        
        await _gameService.UpdateGameAnagramForUser(id, anagramId, updateGame);
        
        return Ok();
    }
}