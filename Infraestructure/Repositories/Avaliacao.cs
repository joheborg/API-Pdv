using API_Pdv.Interfaces.Repositories;
using API_Pdv.Infraestructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using AvaliacaoEntity = API_Pdv.Entities.Avaliacao;

namespace API_Pdv.Infraestructure.Repositories;

public class Avaliacao : IAvaliacao
{
    private readonly ApplicationDbContext _context;

    public Avaliacao(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AvaliacaoEntity> GetByIdAsync(int id)
    {
        return await _context.Avaliacoes
            .Include(a => a.Empresa)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<AvaliacaoEntity>> GetAllAsync()
    {
        return await _context.Avaliacoes
            .Include(a => a.Empresa)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<AvaliacaoEntity>> GetByEmpresaAsync(int empresaId)
    {
        return await _context.Avaliacoes
            .Include(a => a.Empresa)
            .Where(a => a.EmpresaId == empresaId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<AvaliacaoEntity?> GetByNumeroComandaAsync(string numeroComanda, int empresaId)
    {
        return await _context.Avaliacoes
            .Include(a => a.Empresa)
            .FirstOrDefaultAsync(a => a.NumeroComanda == numeroComanda && a.EmpresaId == empresaId);
    }

    public async Task<AvaliacaoEntity> CreateAsync(AvaliacaoEntity avaliacao)
    {
        avaliacao.CreatedAt = DateTime.Now;
        avaliacao.UpdatedAt = DateTime.Now;
        
        _context.Avaliacoes.Add(avaliacao);
        await _context.SaveChangesAsync();
        return avaliacao;
    }

    public async Task<AvaliacaoEntity> UpdateAsync(AvaliacaoEntity avaliacao)
    {
        var existingAvaliacao = await _context.Avaliacoes.FindAsync(avaliacao.Id);
        if (existingAvaliacao == null)
            throw new ArgumentException($"Avaliação com ID {avaliacao.Id} não encontrada");

        existingAvaliacao.Nota = avaliacao.Nota;
        existingAvaliacao.Descricao = avaliacao.Descricao;
        existingAvaliacao.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return existingAvaliacao;
    }

    public async Task DeleteAsync(int id)
    {
        var avaliacao = await _context.Avaliacoes.FindAsync(id);
        if (avaliacao != null)
        {
            _context.Avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();
        }
    }
} 