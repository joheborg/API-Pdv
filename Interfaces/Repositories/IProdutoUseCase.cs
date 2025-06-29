namespace API_Pdv.Interfaces.Repositories;
using ProdutoEntities = API_Pdv.Entities.Produto;

public interface IProdutoUseCase
{
    public Task<ProdutoEntities> GetById(int id);
    public Task<List<ProdutoEntities>> Get();
    public Task<ProdutoEntities> Get(int id);
    public Task<ProdutoEntities> Post(ProdutoEntities produto);
    public Task<ProdutoEntities> Put(ProdutoEntities produto);
    public Task Delete(int id);
}