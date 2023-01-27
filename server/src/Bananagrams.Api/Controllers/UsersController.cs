using System.Net;
using AutoMapper;
using Bananagrams.Api.ViewModels.Users;
using Bananagrams.Service.Dtos.Users;
using Bananagrams.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class Users : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public Users(IUserService userService, IMapper mapper) =>
        (_userService, _mapper) = (userService, mapper);

    [HttpGet]
    public ActionResult<IList<UserViewModel>> GetAll(string? searchWord)
    {
        var users = _userService.GetAll(searchWord);

        var viewModel = _mapper.Map<IList<UserViewModel>>(users);

        return Ok(viewModel);
    }
    
    [HttpGet("{id}")]
    public ActionResult<UserDetailViewModel> Get(int id)
    {
        var user = _userService.Get(id);
    
        if (user == null) return NotFound();

        var viewModel = _mapper.Map<UserDetailViewModel>(user);
        
        return Ok(viewModel);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateUserViewModel userDetails)
    {
        _userService.Create(_mapper.Map<UserDto>(userDetails));
        
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] UpdateUserViewModel userDetails)
    {
        _userService.Update(id, _mapper.Map<UserDto>(userDetails));
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _userService.Delete(id);
        
        return NoContent();
    }
}