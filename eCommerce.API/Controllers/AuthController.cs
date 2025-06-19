using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
  private readonly IUsersService _usersService;
  private readonly IValidator<LoginRequest> _loginValidator;
  private readonly IValidator<RegisterRequest> _registerValidator;
  public AuthController(
    IUsersService usersService, 
    IValidator<LoginRequest> loginValidator,
    IValidator<RegisterRequest> registerValidator
    ) {
    _usersService = usersService;
    _loginValidator = loginValidator;
    _registerValidator = registerValidator;
  }

  //Endpoint for user registration use case.
  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterRequest registerRequest)
  {
    ValidationResult result = await _registerValidator.ValidateAsync(registerRequest);

    if (!result.IsValid)
    {
      return BadRequest(result.Errors);
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
    ValidationResult result = await _loginValidator.ValidateAsync(loginRequest);

    if (!result.IsValid) {
      return BadRequest(result.Errors);
    }

    AuthenticationResponse? authResp = await _usersService.Login(loginRequest);

    if(authResp == null || authResp.Success == false)
    {
      return Unauthorized(authResp);
    }

    return Ok(authResp);
  }
}
