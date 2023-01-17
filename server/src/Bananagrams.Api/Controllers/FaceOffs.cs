using Bananagrams.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;



[ApiController]
[Route("[controller]")]
public class FaceOffs : ControllerBase
{
    private FaceOff faceOff = new FaceOff()
    {
        Id = 1,
        Title = "Sean Vs Niall",
        UserAnagrams = new List<UserAnagram>()
        {
            new UserAnagram() { UserId = 1, AnagramId = 1 },
            new UserAnagram() { UserId = 2, AnagramId = 1 }
            
        }
    };

    [HttpGet]
    public ActionResult<List<FaceOff>> GetAll()
    {
        return Ok(new List<FaceOff>
        {
            faceOff,
            faceOff
        });
    }

    [HttpGet("{id}")]
    public ActionResult<FaceOff> Get(string json)
    {
        return Ok(faceOff);
    }
    
    [HttpPut("{id}")]
    public ActionResult Update(string json)
    {
        return NoContent();
    }
}