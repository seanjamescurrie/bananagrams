using System.Net;
using AutoMapper;
using Bananagrams.Api.ViewModels.Users;
using Bananagrams.Service.Dtos.Users;
using Bananagrams.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bananagrams.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : BananagramsBaseController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public UsersController(IUserService userService, IMapper mapper) =>
        (_userService, _mapper) = (userService, mapper);

    [HttpGet]
    public async Task<ActionResult<IList<UserViewModel>>> GetAll(string? searchWord)
    {
        var users = await _userService.GetAll(searchWord);

        var viewModel = _mapper.Map<List<UserViewModel>>(users);

        return OkOrNoContent(viewModel);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailViewModel>> Get(int id)
    {
        var user = await _userService.Get(id);
        
        var viewModel = _mapper.Map<UserDetailViewModel>(user);
        
        return OkOrNoNotFound(viewModel);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] CreateUserViewModel userDetails)
    {
        await _userService.Create(_mapper.Map<CreateUserDto>(userDetails));
        
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserViewModel userDetails)
    {
        await _userService.Update(id, _mapper.Map<UpdateUserDto>(userDetails));
        
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.Delete(id);
        
        return Ok();
    }
}