using System.Net;
using Bananagrams.Api.Models;
using Bananagrams.Api.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class Users : ControllerBase
{

    [HttpGet]
    public ActionResult<List<UserViewModel>> GetAll()
    {
        return Ok(new List<UserViewModel>
        {
            DataSeed.user,
            DataSeed.user
        });
    }

    [HttpGet("{id}")]
    public ActionResult<UserDetailViewModel> Get(int id)
    {
        return Ok(DataSeed.detailUser);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateUserViewModel userDetails)
    {
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] UpdateUserViewModel userDetails)
    {
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return NoContent();
    }
}