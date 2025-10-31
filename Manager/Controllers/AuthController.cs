using Manager.Web.Models;
using Manager.Web.Services;
using Manager.WebApí.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username == "admin" && request.Password == "1234")
        {
            var token = _tokenService.GenerateToken(request.Username, "Admin");
            return Ok(new LoginResponse(token));
        }

        return Unauthorized("Credenciais inválidas");
    }
}
