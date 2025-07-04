using API_Pdv.Entities;
using API_Pdv.Infraestructure.Data.Context;
using API_Pdv.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API_Pdv.Infraestructure.Repositories;

public class UsuarioRepository : IUsuario
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios
            .Include(u => u.Empresa)
            .Include(u => u.Funcionario)
            .ToListAsync();
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios
            .Include(u => u.Empresa)
            .Include(u => u.Funcionario)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios
            .Include(u => u.Empresa)
            .Include(u => u.Funcionario)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<Usuario>> GetByEmpresaAsync(int empresaId)
    {
        return await _context.Usuarios
            .Include(u => u.Empresa)
            .Include(u => u.Funcionario)
            .Where(u => u.EmpresaId == empresaId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Usuario>> GetAtivosAsync()
    {
        return await _context.Usuarios
            .Include(u => u.Empresa)
            .Include(u => u.Funcionario)
            .Where(u => u.Ativo)
            .ToListAsync();
    }

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        usuario.CreatedAt = DateTime.Now;
        usuario.UpdatedAt = DateTime.Now;
        usuario.Ativo = true;

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        var existingUsuario = await _context.Usuarios.FindAsync(usuario.Id);
        if (existingUsuario == null)
            throw new ArgumentException($"Usuário com ID {usuario.Id} não encontrado");

        existingUsuario.Nome = usuario.Nome;
        existingUsuario.Email = usuario.Email;
        if (!string.IsNullOrEmpty(usuario.Senha))
            existingUsuario.Senha = usuario.Senha;
        existingUsuario.Perfil = usuario.Perfil;
        existingUsuario.Ativo = usuario.Ativo;
        existingUsuario.UltimoAcesso = usuario.UltimoAcesso;
        existingUsuario.FuncionarioId = usuario.FuncionarioId;
        existingUsuario.EmpresaId = usuario.EmpresaId;
        existingUsuario.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return existingUsuario;
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
            throw new ArgumentException($"Usuário com ID {id} não encontrado");

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }
} 