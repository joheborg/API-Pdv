using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_Pdv.Infraestructure.Data.Context;

using PagamentoCaixaEntity = API_Pdv.Entities.PagamentoCaixa;


public class PagamentoCaixa : IPagamentoCaixa
{
    private readonly ApplicationDbContext _context;

    public PagamentoCaixa(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagamentoCaixaEntity> GetByIdAsync(int id)
    {
        return await _context.Set<PagamentoCaixaEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<PagamentoCaixaEntity>> GetAllAsync()
    {
        return await _context.Set<PagamentoCaixaEntity>().ToListAsync();
    }

    public async Task<PagamentoCaixaEntity> CreateAsync(PagamentoCaixaEntity pagamento)
    {
        _context.Set<PagamentoCaixaEntity>().Add(pagamento);
        await _context.SaveChangesAsync();
        return pagamento;
    }

    public async Task<PagamentoCaixaEntity> UpdateAsync(PagamentoCaixaEntity pagamento)
    {
        _context.Set<PagamentoCaixaEntity>().Update(pagamento);
        await _context.SaveChangesAsync();
        return pagamento;
    }

    public async Task DeleteAsync(int id)
    {
        var pagamento = await _context.Set<PagamentoCaixaEntity>().FindAsync(id);
        if (pagamento != null)
        {
            _context.Set<PagamentoCaixaEntity>().Remove(pagamento);
            await _context.SaveChangesAsync();
        }
    }
} 