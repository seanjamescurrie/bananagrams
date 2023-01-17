using System.Net;
using Bananagrams.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class UserAnagrams : ControllerBase
{
    
    private UserAnagram userAnagram = new UserAnagram
    {
        Id = 1,
        Attempts = 3,
        DatePlayed = DateTime.Now,
        MaxAttempts = 5,
        Solved = true,
        TimeAllowed = 0
    };

    [HttpGet]
    public ActionResult<List<UserAnagram>> GetAll()
    {
        return Ok(new List<UserAnagram>
        {
            userAnagram,
            userAnagram
        });
    }

    [HttpGet("{id}")]
    public ActionResult<UserAnagram> Get(string json)
    {
        return Ok(userAnagram);
    }

    [HttpPost]
    public IActionResult Create()
    {
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut]
    public ActionResult Update(int id)
    {
        return NoContent();
    }
}