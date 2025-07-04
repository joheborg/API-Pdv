namespace API_Pdv.Models.Responses;

public class LoginResponse
{
    public bool Sucesso { get; set; }
    public string? Token { get; set; }
    public string? Mensagem { get; set; }
    public UsuarioInfo? Usuario { get; set; }
}

public class UsuarioInfo
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Email { get; set; } = "";
    public string? Perfil { get; set; }
    public int? EmpresaId { get; set; }
    public string? NomeEmpresa { get; set; }
} 