using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Bananagrams.Service.Dtos.Users;
using Bananagrams.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bananagrams.Api.ViewModels.Authentication;
using Microsoft.IdentityModel.Tokens;
using Bananagrams.Api.Authentication;

namespace Bananagrams.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : BananagramsBaseController
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;
    
    public AuthenticationController(IMapper mapper, IAuthenticationService authenticationService) =>
    (_mapper, _authenticationService) = (mapper, authenticationService);

    [HttpPost]
    [AllowAnonymous]
    public ActionResult<AuthenticationResultViewModel> Authenticate([FromBody] AuthenticationRequestViewModel authenticationRequestViewModel)
    {
        var account = _authenticationService.Authenticate(authenticationRequestViewModel.Email, authenticationRequestViewModel.Password);

        if (account is null) return Unauthorized();
        
        return Ok(new AuthenticationResultViewModel
        {
            AccessToken = GenerateToken(account, 600), RefreshToken = GenerateToken(account, 18000)
        });
    }
    
    private string GenerateToken(UserDto user, int expirationTimeInMinutes)
    {
        var secretKey = Encoding.UTF8.GetBytes("JWTMySonTheDayYouWereBorn");
        var securityKey = new SymmetricSecurityKey(secretKey);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var expiryTime = DateTime.UtcNow.AddMinutes(expirationTimeInMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
             
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, "User")
            }),
            Expires = expiryTime,
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
         
        var tokenString = tokenHandler.WriteToken(jwtToken);
        return tokenString;
    }
}