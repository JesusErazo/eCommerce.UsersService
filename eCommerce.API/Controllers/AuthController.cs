using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController: ControllerBase
{
  private readonly IUsersService _usersService;
  public AuthController(IUsersService usersService) {
    _usersService = usersService;
  }

  //Endpoint for user registration use case.
  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterRequest registerRequest)
  {
    if (registerRequest == null)
    {
      return BadRequest("Invalid registration data.");
    }

    AuthenticationResponse? authResp = await _usersService.Register(registerRequest);

    if (authResp == null || authResp.Success == false) {
      return BadRequest(authResp);
    }

    return Ok(authResp);
  }

  //Endpoint for user login use case
  [HttpPost("login")]
  public async Task<IActionResult> Login (LoginRequest loginRequest)
  {
    if (loginRequest == null) {
      return BadRequest("Invalid login data.");  
    }

    AuthenticationResponse? authResp = await _usersService.Login(loginRequest);

    if(authResp == null || authResp.Success == false)
    {
      return Unauthorized(authResp);
    }

    return Ok(authResp);
  }
}
