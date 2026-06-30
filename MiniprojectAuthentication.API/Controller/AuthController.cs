using Microsoft.AspNetCore.Mvc;
using MiniProjectAuthentication.API.Extensions;
using MiniProjectAuthentication.Service.Auth;
using MiniProjectAuthentication.Service.Models;

namespace MiniProjectAuthentication.API.Controller;

[ApiController]
public class AuthController: ControllerBase
{
    private readonly IService _authService;
    
    public  AuthController(IService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("/api/v1/auth/register")]
    public async Task<IActionResult> Register([FromBody]Request.RegisterRequest request)
    {
        var result = await _authService.Register(request);
        return Ok(ApiResponseFactory.Base(result, true,"", HttpContext.TraceIdentifier));
    }
    [HttpPost("/api/v1/auth/login")]
    public async Task<IActionResult> Login([FromBody]Request.LoginRequest request)
    {
        var result = await _authService.Login(request);
        HttpContext.Response.WriteAuthCookies(result.AccescToken, result.RefreshToken);
        return Ok(ApiResponseFactory.Base(result.Message, true,"", HttpContext.TraceIdentifier));
    }
    
    [HttpPost("/api/v1/auth/verify-email")]
    public async Task<IActionResult> Login([FromBody]Request.VerifyEmailRequest request)
    {
        var result = await _authService.VerifyEmail(request);
        return Ok(ApiResponseFactory.Base(result, true,"", HttpContext.TraceIdentifier));
    }
    
}