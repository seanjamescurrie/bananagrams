using Bananagrams.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class Anagrams : ControllerBase
{
    private Anagram anagram = new Anagram
    {
        Id = 1,
        AnagramLength = 5,
        AnagramSolution = "BANANA",
        AnagramWord = "ABANAN",
        DateCreated = DateTime.Now,
        ImageLocation = "http://tropicalfruitandveg.com/images/bananauk2.jpg"
    };

    [HttpGet(Name = "GetAnagrams")]
    [ActionName("GetAll")]
    public ActionResult<List<Anagram>> GetAll()
    {
        return Ok(new List<Anagram>
        {
            anagram,
            anagram
        });
    }

    [HttpGet(Name = "GetAnagram")]
    [ActionName("Get")]
    public ActionResult<Anagram> Get(string json)
    {
        return Ok(anagram);
    }
    
    [HttpPost(Name = "UpdateAnagram")]
    public ActionResult Update(string json)
    {
        return NoContent();
        
    }
    
    [HttpDelete(Name = "DeleteAnagram")]
    public ActionResult Delete(string json)
    {
        return NoContent();
    }
}