using System.Net;
using Bananagrams.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class Users : ControllerBase
{
    private User testUser = new User
    {
        Id = 1,
        EmailAddress = "sean@mail.com",
        FirstName = "Sean",
        LastName = "Currie",
        DateCreated = DateTime.Now
    };

    [HttpGet]
    public ActionResult<List<User>> GetAll()
    {
        return Ok(new List<User>
        {
            testUser,
            testUser
        });
    }

    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        return Ok(testUser);
    }

    [HttpPost]
    public IActionResult Create()
    {
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut("{id}")]
    public ActionResult Update(int id)
    {
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return NoContent();
    }
}