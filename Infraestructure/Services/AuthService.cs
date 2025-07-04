using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API_Pdv.Interfaces.Repositories;
using API_Pdv.Interfaces.Services;
using API_Pdv.Models.Requests;
using API_Pdv.Models.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API_Pdv.Infraestructure.Services;

public class AuthService : IAuthService
{
    private readonly IUsuario _usuarioRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUsuario usuarioRepository, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            // Buscar usuário por email
            var usuario = await _usuarioRepository.GetByEmailAsync(request.Email);
            
            if (usuario == null)
            {
                return new LoginResponse
                {
                    Sucesso = false,
                    Mensagem = "Email ou senha inválidos"
                };
            }

            // Verificar se usuário está ativo
            if (!usuario.Ativo)
            {
                return new LoginResponse
                {
                    Sucesso = false,
                    Mensagem = "Usuário inativo"
                };
            }

            // Validar senha
            if (!(request.Senha == usuario.Senha))
            {
                return new LoginResponse
                {
                    Sucesso = false,
                    Mensagem = "Email ou senha inválidos"
                };
            }

            // Atualizar último acesso
            usuario.UltimoAcesso = DateTime.Now;
            await _usuarioRepository.UpdateAsync(usuario);

            // Gerar token JWT
            var usuarioInfo = new UsuarioInfo
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil,
                EmpresaId = usuario.EmpresaId,
                NomeEmpresa = usuario.Empresa?.RazaoSocial
            };

            var token = GenerateJwtToken(usuarioInfo);

            return new LoginResponse
            {
                Sucesso = true,
                Token = token,
                Usuario = usuarioInfo,
                Mensagem = "Login realizado com sucesso"
            };
        }
        catch (Exception ex)
        {
            return new LoginResponse
            {
                Sucesso = false,
                Mensagem = $"Erro interno: {ex.Message}"
            };
        }
    }

    public string GenerateJwtToken(UsuarioInfo usuario)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "SuaChaveSecretaAqui123456789"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim("Perfil", usuario.Perfil ?? ""),
            new Claim("EmpresaId", usuario.EmpresaId?.ToString() ?? ""),
            new Claim("NomeEmpresa", usuario.NomeEmpresa ?? "")
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"] ?? "WebPdv",
            audience: _configuration["Jwt:Audience"] ?? "WebPdv",
            claims: claims,
            expires: DateTime.Now.AddHours(8), // Token válido por 8 horas
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidatePassword(string senhaDigitada, string senhaHash)
    {
        var senhaHashDigitada = HashPassword(senhaDigitada);
        return senhaHashDigitada == senhaHash;
    }

    public string HashPassword(string senha)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(hashedBytes);
        }
    }
} 