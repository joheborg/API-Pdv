using API_Pdv.Interfaces.Services;
using API_Pdv.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebPdv.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authService.LoginAsync(request);

            if (!response.Sucesso)
                return Unauthorized(response);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Sucesso = false, Mensagem = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // Em uma implementação mais robusta, você poderia invalidar o token
        // Por enquanto, apenas retorna sucesso
        return Ok(new { Sucesso = true, Mensagem = "Logout realizado com sucesso" });
    }
} 