using Microsoft.EntityFrameworkCore;
using EmpresaEntities = API_Pdv.Entities.Empresa;
using ProdutoEntities = API_Pdv.Entities.Produto;
namespace API_Pdv.Infraestructure.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ProdutoEntities> Produtos { get; set; }
    public DbSet<EmpresaEntities> Empresas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}