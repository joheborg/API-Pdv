using System.Collections.Generic;
using System.Threading.Tasks;
using ItemPedidoEntity = API_Pdv.Entities.ItemPedido;

public interface IItemPedido
{
    Task<ItemPedidoEntity> GetByIdAsync(int id);
    Task<IEnumerable<ItemPedidoEntity>> GetAllAsync();
    Task<ItemPedidoEntity> CreateAsync(ItemPedidoEntity item);
    Task<ItemPedidoEntity> UpdateAsync(ItemPedidoEntity item);
    Task DeleteAsync(int id);
} 