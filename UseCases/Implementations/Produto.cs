using API_Pdv.Interfaces.Repositories;
using ProdutoEntities = API_Pdv.Entities.Produto;
namespace API_Pdv.UseCases.Implementations;

public class Produto(IProduto produto):IProdutoUseCase
{

    public async Task<ProdutoEntities> GetById(int id)
    {
        return await produto.GetByIdAsync(id);
    }
    public async Task<List<ProdutoEntities>> Get()
    {
        return (List<ProdutoEntities>)await produto.GetAllAsync();
    }
    public async Task<ProdutoEntities> Get(int id)
    {
        return await produto.GetByIdAsync(id);
    }
    public async Task<ProdutoEntities> Post(ProdutoEntities produtoEntities)
    {
        return await produto.CreateAsync(produtoEntities);
    }
    public async Task<ProdutoEntities> Put(ProdutoEntities produtoEntities)
    {
        return await produto.UpdateAsync(produtoEntities);
    }
    public async Task Delete(int id)
    {
        await produto.DeleteAsync(id);
    }
    
}