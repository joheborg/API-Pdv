using API_Pdv.Entities;

namespace API_Pdv.Interfaces.Repositories;

public interface IUsuario
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<IEnumerable<Usuario>> GetByEmpresaAsync(int empresaId);
    Task<IEnumerable<Usuario>> GetAtivosAsync();
    Task<Usuario> CreateAsync(Usuario usuario);
    Task<Usuario> UpdateAsync(Usuario usuario);
    Task DeleteAsync(int id);
} 