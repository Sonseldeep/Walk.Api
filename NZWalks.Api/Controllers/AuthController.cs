using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Models.DTO;

namespace NZWalks.Api.Controllers;

[ApiController]

public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    //ctor garaune anni UserManager pass garne microsoft le provide gareko
    public AuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
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
}
