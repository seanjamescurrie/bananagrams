using System.Net;
using Bananagrams.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
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

    [HttpGet(Name = "GetAllUSers")]
    [ActionName("GetAll")]
    public ActionResult<List<User>> GetAll()
    {
        return Ok(new List<User>
        {
            testUser,
            testUser
        });
    }

    [HttpGet(Name = "GetUser")]
    [ActionName("Get")]
    public ActionResult<User> Get(string json)
    {
        return Ok(testUser);
    }
    
    [HttpPost(Name = "UpdateUser")]
    public ActionResult Update(string json)
    {
        return StatusCode((int)HttpStatusCode.Created);
        
        return NoContent();
    }
    
    [HttpDelete(Name = "DeleteUser")]
    public ActionResult Delete(string json)
    {
        return NoContent();
    }
}