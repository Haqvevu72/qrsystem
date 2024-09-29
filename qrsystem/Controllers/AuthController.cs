using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using qrsystem.Services.AuthServices;
using LoginRequest = qrsystem.Models.Dtos.Auth.LoginRequest;
using RefreshRequest = qrsystem.Models.Dtos.Auth.RefreshRequest;
using RegisterRequest = qrsystem.Models.Dtos.Auth.RegisterRequest;

namespace qrsystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService): ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)
    {
        try
        {
            var result = await _authService.RegisterAsync(model);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var response = await _authService.LoginAsync(model);
        if (response == null) return Unauthorized("Invalid email or password.");

        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest model)
    {
        var response = await _authService.RefreshTokenAsync(model);
        if (response == null) return Unauthorized("Invalid or expired refresh token.");

        return Ok(response);
    }
}