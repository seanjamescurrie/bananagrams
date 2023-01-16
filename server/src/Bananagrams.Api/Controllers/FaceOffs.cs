using Bananagrams.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;



[ApiController]
[Route("[controller]/[action]")]
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

    [HttpGet(Name = "GetAllFaceOffs")]
    [ActionName("GetAll")]
    public ActionResult<List<FaceOff>> GetAll()
    {
        return Ok(new List<FaceOff>
        {
            faceOff,
            faceOff
        });
    }

    [HttpGet(Name = "GetFaceOff")]
    [ActionName("Get")]
    public ActionResult<FaceOff> Get(string json)
    {
        return Ok(faceOff);
    }
    
    [HttpPost(Name = "Update")]
    public ActionResult Update(string json)
    {
        return NoContent();
    }
}