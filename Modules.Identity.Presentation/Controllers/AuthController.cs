using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Modules.Identity.Contracts.AuthDTOs;
using Modules.Identity.Contracts.Services;

namespace Modules.Identity.Controllers;

[ApiExplorerSettings(GroupName = "identity")]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        return StatusCode(201, new ApiResponseModel(true, 201, "User registered successfully", result));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        return Ok(new ApiResponseModel(true, 200, "Login successful", result));
    }

    // No [Authorize]: the access token is usually already expired when this is called.
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest request)
    {
        var result = await _authService.RefreshAsync(request);
        return Ok(new ApiResponseModel(true, 200, "Token refreshed successfully", result));
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(RefreshRequest request)
    {
        await _authService.LogoutAsync(request);
        return Ok(new ApiResponseModel(true, 200, "Logged out successfully"));
    }
}