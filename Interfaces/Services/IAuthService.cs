using API_Pdv.Models.Requests;
using API_Pdv.Models.Responses;

namespace API_Pdv.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    string GenerateJwtToken(UsuarioInfo usuario);
    bool ValidatePassword(string senhaDigitada, string senhaHash);
    string HashPassword(string senha);
} 