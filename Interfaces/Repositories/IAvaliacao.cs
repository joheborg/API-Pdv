using System.Collections.Generic;
using System.Threading.Tasks;
using AvaliacaoEntity = API_Pdv.Entities.Avaliacao;

namespace API_Pdv.Interfaces.Repositories;

public interface IAvaliacao
{
    Task<AvaliacaoEntity> GetByIdAsync(int id);
    Task<IEnumerable<AvaliacaoEntity>> GetAllAsync();
    Task<IEnumerable<AvaliacaoEntity>> GetByEmpresaAsync(int empresaId);
    Task<AvaliacaoEntity?> GetByNumeroComandaAsync(string numeroComanda, int empresaId);
    Task<AvaliacaoEntity> CreateAsync(AvaliacaoEntity avaliacao);
    Task<AvaliacaoEntity> UpdateAsync(AvaliacaoEntity avaliacao);
    Task DeleteAsync(int id);
} 