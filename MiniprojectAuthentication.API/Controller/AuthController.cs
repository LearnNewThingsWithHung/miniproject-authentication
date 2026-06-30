using Microsoft.AspNetCore.Mvc;
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
    
}