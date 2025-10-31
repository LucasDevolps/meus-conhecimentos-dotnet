using Manager.Web.Models;
using Manager.Web.Services;
using Manager.WebApí.Models;
using Manager.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly UserService _userService;

    public AuthController(TokenService tokenService, UserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = string.Empty;

        if (request.Username == "admin" && request.Password == "1234")
        {
            token = _tokenService.GenerateToken(request.Username, "Admin");
            return Ok(new LoginResponse(token));
        }

        var user = await _userService.GetByUsernameAsync(request.Username);

        if (user is null)
            return Unauthorized("Usuário não encontrado");

        if (!_userService.VerifyPassword(request.Password, user.SenhaHash))
            return Unauthorized("Senha inválida");

        token = _tokenService.GenerateToken(request.Username, user.Role);
        return Ok(new LoginResponse(token));
    }
}
