using API_Pdv.Entities;

namespace API_Pdv.Interfaces.Repositories;

public interface IStatusPedido
{
    Task<IEnumerable<StatusPedido>> GetAllAsync();
    Task<StatusPedido?> GetByIdAsync(int id);
    Task<StatusPedido> CreateAsync(StatusPedido statusPedido);
    Task<StatusPedido> UpdateAsync(StatusPedido statusPedido);
    Task DeleteAsync(int id);
} 