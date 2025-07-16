using Microsoft.EntityFrameworkCore;
using EmpresaEntities = API_Pdv.Entities.Empresa;
using ProdutoEntities = API_Pdv.Entities.Produto;
using CategoriaEntities = API_Pdv.Entities.Categoria;
using StatusPedidoEntities = API_Pdv.Entities.StatusPedido;
using PedidoEntities = API_Pdv.Entities.Pedido;
using ItemPedidoEntities = API_Pdv.Entities.ItemPedido;
using VendaEntities = API_Pdv.Entities.Venda;
using ItemVendaEntities = API_Pdv.Entities.ItemVenda;
using TransacaoEntities = API_Pdv.Entities.Transacao;
using ClienteEntities = API_Pdv.Entities.Cliente;
using UsuarioEntities = API_Pdv.Entities.Usuario;
using AtividadeRecenteEntities = API_Pdv.Entities.AtividadeRecente;
using AvaliacaoEntities = API_Pdv.Entities.Avaliacao;

namespace API_Pdv.Infraestructure.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ProdutoEntities> Produtos { get; set; }
    public DbSet<EmpresaEntities> Empresas { get; set; }
    public DbSet<CategoriaEntities> Categorias { get; set; }
    public DbSet<StatusPedidoEntities> StatusPedidos { get; set; }
    public DbSet<PedidoEntities> Pedidos { get; set; }
    public DbSet<ItemPedidoEntities> ItensPedido { get; set; }
    public DbSet<VendaEntities> Vendas { get; set; }
    public DbSet<ItemVendaEntities> ItensVenda { get; set; }
    public DbSet<TransacaoEntities> Transacoes { get; set; }
    public DbSet<ClienteEntities> Clientes { get; set; }
    public DbSet<UsuarioEntities> Usuarios { get; set; }
    public DbSet<AtividadeRecenteEntities> AtividadesRecentes { get; set; }
    public DbSet<AvaliacaoEntities> Avaliacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}