using API_Pdv.Entities;
using API_Pdv.Infraestructure.Data.Context;
using API_Pdv.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API_Pdv.Infraestructure.Repositories;

public class CategoriaRepository : ICategoria
{
    private readonly ApplicationDbContext _context;

    public CategoriaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _context.Categorias.ToListAsync();
    }

    public async Task<Categoria?> GetByIdAsync(int id)
    {
        return await _context.Categorias.FindAsync(id);
    }

    public async Task<Categoria> CreateAsync(Categoria categoria)
    {
        categoria.CreatedAt = DateTime.Now;
        categoria.UpdatedAt = DateTime.Now;
        
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> UpdateAsync(Categoria categoria)
    {
        var existingCategoria = await _context.Categorias.FindAsync(categoria.Id);
        if (existingCategoria == null)
            throw new ArgumentException($"Categoria com ID {categoria.Id} não encontrada");

        existingCategoria.Descricao = categoria.Descricao;
        existingCategoria.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return existingCategoria;
    }

    public async Task DeleteAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
            throw new ArgumentException($"Categoria com ID {id} não encontrada");

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
    }
} 