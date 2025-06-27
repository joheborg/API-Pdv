namespace API_Pdv.Interfaces.Repositories;
using API_Pdv.Entities;
public interface IProdutoUseCase
{
    public Task<Produto> GetById(int id);
    public Task<List<Produto>> Get();
    public Task<Produto> Get(int id);
    public Task<Produto> Post(Produto produto);
    public Task<Produto> Put(Produto produto);
    public Task Delete(int id);
}