using Bananagrams.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;


[ApiController]
[Route("[controller]/[action]")]
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

    [HttpGet(Name = "GetAllAnagrams")]
    [ActionName("GetAll")]
    public ActionResult<List<UserAnagram>> GetAll()
    {
        return Ok(new List<UserAnagram>
        {
            userAnagram,
            userAnagram
        });
    }

    [HttpGet(Name = "GetAnagram")]
    [ActionName("Get")]
    public ActionResult<UserAnagram> Get(string json)
    {
        return Ok(userAnagram);
    }
    
    [HttpPost(Name = "Update")]
    public ActionResult Update(string json)
    {
        return NoContent();
    }
}