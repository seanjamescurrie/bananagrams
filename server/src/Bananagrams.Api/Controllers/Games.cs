using System.Net;
using Bananagrams.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;



[ApiController]
[Route("[controller]")]
public class Games : ControllerBase
{
    private Game game = new Game()
    {
        Id = 1,
        Title = "Sean Vs Niall",
        DateCreated = DateTime.Now,
        
        // UserAnagrams = new List<UserAnagram>()
        // {
        //     new UserAnagram() { UserId = 1, AnagramId = 1 },
        //     new UserAnagram() { UserId = 2, AnagramId = 1 }
        //     
        // }
    };

    [HttpGet]
    public ActionResult<List<Game>> GetAll()
    {
        return Ok(new List<Game>
        {
            game,
            game
        });
    }

    [HttpGet("{id}")]
    public ActionResult<Game> Get()
    {
        return Ok(game);
    }
    
    [HttpPost]
    public IActionResult Create()
    {
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut("{id}/Attempt/{anagramId}")]
    public IActionResult UpdateGameUserAnagramAttempt()
    {
        return Ok();
    }
}