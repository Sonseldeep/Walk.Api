using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repositories;

namespace NZWalks.Api.Controllers;

[ApiController]

public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenRepository _tokenRepository;

    //ctor garaune anni UserManager pass garne microsoft le provide gareko
    public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
    }
    // Post: /api/auth/register
    [HttpPost("api/auth/register")]

    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        var identityUser = new IdentityUser 
        {
            UserName = registerRequestDto.Username,
            Email = registerRequestDto.Username
        };
            
      var identityResult =  await _userManager.CreateAsync(identityUser, registerRequestDto.Password);
      if (identityResult.Succeeded)
      {
          // Add roles to this user
          if (registerRequestDto.Roles is not null && registerRequestDto.Roles.Any())
          {
              identityResult = await _userManager.AddToRoleAsync(identityUser, registerRequestDto.Roles);
              if (identityResult.Succeeded)
              {
                  return Ok("User registered successfully! Please login.");
              }
          }
      }

      return BadRequest("Something went wrong");

            
      }
    
    
    // Post: /api/auth/login
    [HttpPost("api/auth/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
       var user =   await _userManager.FindByNameAsync(loginRequestDto.Username);
       if (user is null) return BadRequest("Username or password is incorrect");
       var checkPasswordResult =  await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
       if (checkPasswordResult)
       {
           // Get roles of the user
           var roles = await _userManager.GetRolesAsync(user);
           if (roles is not null)
           {
             var jwtToken =   _tokenRepository.CreateJWTToken(user,roles.ToList());
             var response = new LoginResponseDto
             {
                 JwtToken = jwtToken
             };
             return Ok(response);
           }
           
       }
       return BadRequest("Username or password is incorrect");
        
    }
}
