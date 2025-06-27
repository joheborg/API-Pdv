using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_Pdv.Infraestructure.Data.Context;

using PedidoEntity = API_Pdv.Entities.Pedido;


public class Pedido : IPedido
{
    private readonly ApplicationDbContext _context;

    public Pedido(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PedidoEntity> GetByIdAsync(int id)
    {
        return await _context.Set<PedidoEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<PedidoEntity>> GetAllAsync()
    {
        return await _context.Set<PedidoEntity>().ToListAsync();
    }

    public async Task<PedidoEntity> CreateAsync(PedidoEntity pedido)
    {
        _context.Set<PedidoEntity>().Add(pedido);
        await _context.SaveChangesAsync();
        return pedido;
    }

    public async Task<PedidoEntity> UpdateAsync(PedidoEntity pedido)
    {
        _context.Set<PedidoEntity>().Update(pedido);
        await _context.SaveChangesAsync();
        return pedido;
    }

    public async Task DeleteAsync(int id)
    {
        var pedido = await _context.Set<PedidoEntity>().FindAsync(id);
        if (pedido != null)
        {
            _context.Set<PedidoEntity>().Remove(pedido);
            await _context.SaveChangesAsync();
        }
    }
} 