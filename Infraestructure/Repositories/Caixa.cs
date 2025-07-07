using System.Collections.Generic;
using System.Threading.Tasks;
using API_Pdv.Infraestructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using CaixaEntity= API_Pdv.Entities.Caixa;

namespace API_Pdv.Infraestructure.Repositories;

public class Caixa : ICaixa
{
    private readonly ApplicationDbContext _context;

    public Caixa(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CaixaEntity> GetByIdAsync(int id)
    {
        return await _context.Set<CaixaEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<CaixaEntity>> GetAllAsync()
    {
        return await _context.Set<CaixaEntity>().ToListAsync();
    }

    public async Task<IEnumerable<CaixaEntity>> GetByEmpresaAsync(int empresaId)
    {
        return await _context.Set<CaixaEntity>()
            .Include(c => c.Empresa)
            .Where(c => c.EmpresaId == empresaId)
            .OrderByDescending(c => c.DataAbertura)
            .ToListAsync();
    }

    public async Task<CaixaEntity> CreateAsync(CaixaEntity caixa)
    {
        _context.Set<CaixaEntity>().Add(caixa);
        await _context.SaveChangesAsync();
        return caixa;
    }

    public async Task<CaixaEntity> UpdateAsync(CaixaEntity caixa)
    {
        _context.Set<CaixaEntity>().Update(caixa);
        await _context.SaveChangesAsync();
        return caixa;
    }

    public async Task DeleteAsync(int id)
    {
        var caixa = await _context.Set<CaixaEntity>().FindAsync(id);
        if (caixa != null)
        {
            _context.Set<CaixaEntity>().Remove(caixa);
            await _context.SaveChangesAsync();
        }
    }
} 