using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_Pdv.Infraestructure.Data.Context;

using ItemPedidoEntity = API_Pdv.Entities.ItemPedido;

public class ItemPedido : IItemPedido
{
    private readonly ApplicationDbContext _context;

    public ItemPedido(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemPedidoEntity> GetByIdAsync(int id)
    {
        return await _context.Set<ItemPedidoEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<ItemPedidoEntity>> GetAllAsync()
    {
        return await _context.Set<ItemPedidoEntity>().ToListAsync();
    }

    public async Task<ItemPedidoEntity> CreateAsync(ItemPedidoEntity item)
    {
        _context.Set<ItemPedidoEntity>().Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<ItemPedidoEntity> UpdateAsync(ItemPedidoEntity item)
    {
        _context.Set<ItemPedidoEntity>().Update(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Set<ItemPedidoEntity>().FindAsync(id);
        if (item != null)
        {
            _context.Set<ItemPedidoEntity>().Remove(item);
            await _context.SaveChangesAsync();
        }
    }
} 