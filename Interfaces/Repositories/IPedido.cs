using System.Collections.Generic;
using System.Threading.Tasks;
using PedidoEntity = API_Pdv.Entities.Pedido;

namespace API_Pdv.Interfaces.Repositories;

public interface IPedido
{
    Task<PedidoEntity> GetByIdAsync(int id);
    Task<IEnumerable<PedidoEntity>> GetAllAsync();
    Task<PedidoEntity> CreateAsync(PedidoEntity pedido);
    Task<PedidoEntity> UpdateAsync(PedidoEntity pedido);
    Task DeleteAsync(int id);
} 