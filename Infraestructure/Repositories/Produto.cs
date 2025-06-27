using API_Pdv.Infraestructure.Data.Context;
using API_Pdv.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using ProdutoEntities = API_Pdv.Entities.Produto;
namespace API_Pdv.Infraestructure.Repositories;

public class Produto : IProduto
{
    private readonly ApplicationDbContext _context;

    public Produto(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProdutoEntities> GetByIdAsync(int id)
    {
        return await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<ProdutoEntities>> GetAllAsync()
    {
        return await _context.Produtos
            .ToListAsync();
    }

    public async Task<IEnumerable<ProdutoEntities>> GetByEmpresaAsync(int empresaId)
    {
        return await _context.Produtos
            .Where(p => p.EmpresaId == empresaId)
            .ToListAsync();
    }

    public async Task<ProdutoEntities> CreateAsync(ProdutoEntities produto)
    {
        produto.CreatedAt = DateTime.Now;
        produto.UpdatedAt = DateTime.Now;
        produto.Situacao = true; // Produto ativo por padrão

        // Definir valores padrão para campos fiscais se não fornecidos
        produto.CstIcms ??= "00";
        produto.BaseCalculoIcms ??= 0.00m;
        produto.AliquotaIcms ??= 0.00m;
        produto.ValorIcms ??= 0.00m;

        produto.CstIpi ??= "50";
        produto.BaseCalculoIpi ??= 0.00m;
        produto.AliquotaIpi ??= 0.00m;
        produto.ValorIpi ??= 0.00m;

        produto.CstPis ??= "01";
        produto.BaseCalculoPis ??= 0.00m;
        produto.AliquotaPis ??= 1.65m;
        produto.ValorPis ??= 0.00m;

        produto.CstCofins ??= "01";
        produto.BaseCalculoCofins ??= 0.00m;
        produto.AliquotaCofins ??= 7.60m;
        produto.ValorCofins ??= 0.00m;

        produto.UnidadeVenda ??= "UN";

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return produto;
    }

    public async Task<ProdutoEntities> UpdateAsync(ProdutoEntities produto)
    {
        var existingProduto = await _context.Produtos.FindAsync(produto.Id);
        if (existingProduto == null)
            throw new ArgumentException("Produto não encontrado");

        // Atualizar propriedades básicas
        existingProduto.CodigoProduto = produto.CodigoProduto;
        existingProduto.ImagemUrl = produto.ImagemUrl;
        existingProduto.ImagemBase64 = produto.ImagemBase64;
        existingProduto.ImagemNome = produto.ImagemNome;
        existingProduto.ImagemMimeType = produto.ImagemMimeType;
        existingProduto.Nome = produto.Nome;
        existingProduto.NomeAlternativo = produto.NomeAlternativo;
        existingProduto.Descricao = produto.Descricao;
        existingProduto.Categoria = produto.Categoria;
        existingProduto.Ingredientes = produto.Ingredientes;
        existingProduto.Situacao = produto.Situacao;

        // Dados comerciais
        existingProduto.PrecoVenda = produto.PrecoVenda;
        existingProduto.PrecoCusto = produto.PrecoCusto;
        existingProduto.QuantidadePadrao = produto.QuantidadePadrao;
        existingProduto.Peso = produto.Peso;
        existingProduto.ServePessoas = produto.ServePessoas;
        existingProduto.CodigoBarras = produto.CodigoBarras;
        existingProduto.UnidadeVenda = produto.UnidadeVenda;

        // Dados fiscais básicos
        existingProduto.NCM = produto.NCM;
        existingProduto.CEST = produto.CEST;
        existingProduto.CFOP = produto.CFOP;
        existingProduto.CSOSN_CST = produto.CSOSN_CST;
        existingProduto.OrigemProduto = produto.OrigemProduto;

        // Dados fiscais ICMS
        existingProduto.CstIcms = produto.CstIcms;
        existingProduto.BaseCalculoIcms = produto.BaseCalculoIcms;
        existingProduto.AliquotaIcms = produto.AliquotaIcms;
        existingProduto.ValorIcms = produto.ValorIcms;

        // Dados fiscais IPI
        existingProduto.CstIpi = produto.CstIpi;
        existingProduto.BaseCalculoIpi = produto.BaseCalculoIpi;
        existingProduto.AliquotaIpi = produto.AliquotaIpi;
        existingProduto.ValorIpi = produto.ValorIpi;

        // Dados fiscais PIS
        existingProduto.CstPis = produto.CstPis;
        existingProduto.BaseCalculoPis = produto.BaseCalculoPis;
        existingProduto.AliquotaPis = produto.AliquotaPis;
        existingProduto.ValorPis = produto.ValorPis;

        // Dados fiscais COFINS
        existingProduto.CstCofins = produto.CstCofins;
        existingProduto.BaseCalculoCofins = produto.BaseCalculoCofins;
        existingProduto.AliquotaCofins = produto.AliquotaCofins;
        existingProduto.ValorCofins = produto.ValorCofins;

        // Códigos adicionais
        existingProduto.CodigoEan = produto.CodigoEan;
        existingProduto.InformacoesAdicionais = produto.InformacoesAdicionais;

        existingProduto.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return existingProduto;
    }

    public async Task DeleteAsync(int id)
    {
        var existingProduto = await _context.Produtos.FindAsync(id) ?? throw new ArgumentException("Produto não encontrado");
        existingProduto.Situacao = false;
        existingProduto.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
    }

    public async Task<ProdutoEntities> GetByCodigoAsync(int empresaId, string codigoProduto)
    {
        return await _context.Produtos
            .FirstOrDefaultAsync(p => p.EmpresaId == empresaId && p.CodigoProduto == codigoProduto);
    }

    public async Task<ProdutoEntities> GetByCodigoBarrasAsync(string codigoBarras)
    {
        return await _context.Produtos
            .FirstOrDefaultAsync(p => p.CodigoBarras == codigoBarras);
    }

    public async Task<ProdutoEntities> GetByCodigoEanAsync(string codigoEan)
    {
        return await _context.Produtos
            .FirstOrDefaultAsync(p => p.CodigoEan == codigoEan);
    }
}