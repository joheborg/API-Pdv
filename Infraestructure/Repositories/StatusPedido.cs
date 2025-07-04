using API_Pdv.Entities;
using API_Pdv.Infraestructure.Data.Context;
using API_Pdv.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API_Pdv.Infraestructure.Repositories;

public class StatusPedidoRepository : IStatusPedido
{
    private readonly ApplicationDbContext _context;

    public StatusPedidoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StatusPedido>> GetAllAsync()
    {
        return await _context.StatusPedidos.ToListAsync();
    }

    public async Task<StatusPedido?> GetByIdAsync(int id)
    {
        return await _context.StatusPedidos.FindAsync(id);
    }

    public async Task<StatusPedido> CreateAsync(StatusPedido statusPedido)
    {
        statusPedido.CreatedAt = DateTime.Now;
        statusPedido.UpdatedAt = DateTime.Now;
        _context.StatusPedidos.Add(statusPedido);
        await _context.SaveChangesAsync();
        return statusPedido;
    }

    public async Task<StatusPedido> UpdateAsync(StatusPedido statusPedido)
    {
        var existing = await _context.StatusPedidos.FindAsync(statusPedido.Id);
        if (existing == null)
            throw new ArgumentException($"StatusPedido com ID {statusPedido.Id} não encontrado");
        existing.Descricao = statusPedido.Descricao;
        existing.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task DeleteAsync(int id)
    {
        var statusPedido = await _context.StatusPedidos.FindAsync(id);
        if (statusPedido == null)
            throw new ArgumentException($"StatusPedido com ID {id} não encontrado");
        _context.StatusPedidos.Remove(statusPedido);
        await _context.SaveChangesAsync();
    }
} 