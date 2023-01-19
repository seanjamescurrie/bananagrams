using System.Net;
using Bananagrams.Api.Dtos;
using Bananagrams.Api.Models;
using Bananagrams.Api;
using Bananagrams.Api.Models.Games;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;



[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{

    private readonly IBananagramsDatabase _database;
    public GamesController(IBananagramsDatabase database) => _database = database;

    [HttpGet]
    public ActionResult<List<GameViewModel>> GetAll()
    {
        var games = _database.Get<Game>().ToList();
        
        return Ok(games);
    }

    [HttpGet("{id}")]
    public ActionResult<GameDetailViewModel> Get(int id)
    {
        // var game = _database.Get<Game>(id);
        return Ok(DataSeed.gameDetail);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateGameViewModel gameDetails)
    {
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut("{id}/Attempt/{anagramId}")]
    public IActionResult UpdateGameUserAnagramAttempt(int id, int anagramId, [FromBody] UpdateGameViewModel gameDetails)
    {
        return Ok();
    }
}