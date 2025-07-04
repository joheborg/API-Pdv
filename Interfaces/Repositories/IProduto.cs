using ProdutoEntities = API_Pdv.Entities.Produto;

namespace API_Pdv.Interfaces.Repositories;

public interface IProduto
{
    Task<ProdutoEntities> GetByIdAsync(int id);
    Task<IEnumerable<ProdutoEntities>> GetAllAsync();
    Task<IEnumerable<ProdutoEntities>> GetByEmpresaAsync(int empresaId);
    Task<ProdutoEntities> CreateAsync(ProdutoEntities produto);
    Task<ProdutoEntities> UpdateAsync(ProdutoEntities produto);
    Task DeleteAsync(int id);
    Task<ProdutoEntities> GetByCodigoAsync(int empresaId, string codigoProduto);
    Task<ProdutoEntities> GetByCodigoBarrasAsync(string codigoBarras);
    Task<ProdutoEntities> GetByCodigoEanAsync(string codigoEan);
    Task<IEnumerable<ProdutoEntities>> GetByCategoriaAsync(int categoriaId);
}