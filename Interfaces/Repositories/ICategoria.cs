using API_Pdv.Entities;

namespace API_Pdv.Interfaces.Repositories;

public interface ICategoria
{
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task<Categoria?> GetByIdAsync(int id);
    Task<Categoria> CreateAsync(Categoria categoria);
    Task<Categoria> UpdateAsync(Categoria categoria);
    Task DeleteAsync(int id);
} 