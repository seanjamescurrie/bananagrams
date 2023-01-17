using System.Net;
using Bananagrams.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet]
    public ActionResult<List<Anagram>> GetAll()
    {
        return Ok(new List<Anagram>
        {
            anagram,
            anagram
        });
    }

    [HttpGet("{id}")]
    public ActionResult<Anagram> Get(int id)
    {
        return Ok(anagram);
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
    public ActionResult Delete(string json)
    {
        return NoContent();
    }
}