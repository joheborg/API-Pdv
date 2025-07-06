using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_Pdv.Infraestructure.Data.Context;

using PedidoEntity = API_Pdv.Entities.Pedido;
using API_Pdv.Interfaces.Repositories;

namespace API_Pdv.Infraestructure.Repositories;
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

    public async Task<PedidoEntity?> GetByNumeroComandaAsync(string numeroComanda, int empresaId)
    {
        return await _context.Set<PedidoEntity>()
            .Include(p => p.ItensPedido)
            .ThenInclude(i => i.Produto)
            .Include(p => p.Situacao)
            .Include(p => p.Empresa)
            .FirstOrDefaultAsync(p => p.NumeroComanda == numeroComanda && p.EmpresaId == empresaId);
    }

    public async Task<IEnumerable<PedidoEntity>> GetPedidosAbertosAsync(int empresaId)
    {
        var statusAbertos = new[] { "Pendente", "Em Preparo", "Pronto" };
        
        return await _context.Set<PedidoEntity>()
            .Include(p => p.ItensPedido)
            .ThenInclude(i => i.Produto)
            .Include(p => p.Situacao)
            .Include(p => p.Empresa)
            .Where(p => p.EmpresaId == empresaId && statusAbertos.Contains(p.Status))
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();
    }
} 