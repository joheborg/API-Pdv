using API_Pdv.Infraestructure.Data.Context;
using API_Pdv.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using EmpresaEntities = API_Pdv.Entities.Empresa;

namespace API_Pdv.Infraestructure.Repositories;

public class Empresa : IEmpresa
{
    private readonly ApplicationDbContext _context;

    public Empresa(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EmpresaEntities> GetByIdAsync(int id)
    {
        return await _context.Empresas
            .Include(e => e.Endereco)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<EmpresaEntities>> GetAllAsync()
    {
        return await _context.Empresas
            .Include(e => e.Endereco)
            .ToListAsync();
    }

    public async Task<EmpresaEntities> CreateAsync(EmpresaEntities empresa)
    {
        empresa.CreatedAt = DateTime.Now;
        empresa.UpdatedAt = DateTime.Now;
        
        _context.Empresas.Add(empresa);
        await _context.SaveChangesAsync();
        
        return empresa;
    }

    public async Task<EmpresaEntities> UpdateAsync(EmpresaEntities empresa)
    {
        var existingEmpresa = await _context.Empresas.FindAsync(empresa.Id);
        if (existingEmpresa == null)
            throw new ArgumentException("Empresa não encontrada");

        existingEmpresa.CNPJ = empresa.CNPJ;
        existingEmpresa.RazaoSocial = empresa.RazaoSocial;
        existingEmpresa.NomeFantasia = empresa.NomeFantasia;
        existingEmpresa.InscricaoEstadual = empresa.InscricaoEstadual;
        existingEmpresa.CRT = empresa.CRT;
        existingEmpresa.LogoBase64 = empresa.LogoBase64;
        existingEmpresa.LogoNome = empresa.LogoNome;
        existingEmpresa.LogoMimeType = empresa.LogoMimeType;
        existingEmpresa.Endereco = empresa.Endereco;
        existingEmpresa.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        
        return existingEmpresa;
    }

    public async Task DeleteAsync(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);
        if (empresa != null)
        {
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<EmpresaEntities> GetByCnpjAsync(string cnpj)
    {
        return await _context.Empresas
            .Include(e => e.Endereco)
            .FirstOrDefaultAsync(e => e.CNPJ == cnpj);
    }
}