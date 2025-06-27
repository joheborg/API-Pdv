using System.Collections.Generic;
using System.Threading.Tasks;
using PagamentoCaixaEntity = API_Pdv.Entities.PagamentoCaixa;
public interface IPagamentoCaixa
{
    Task<PagamentoCaixaEntity> GetByIdAsync(int id);
    Task<IEnumerable<PagamentoCaixaEntity>> GetAllAsync();
    Task<PagamentoCaixaEntity> CreateAsync(PagamentoCaixaEntity pagamento);
    Task<PagamentoCaixaEntity> UpdateAsync(PagamentoCaixaEntity pagamento);
    Task DeleteAsync(int id);
} 