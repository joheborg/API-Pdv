using System.Collections.Generic;
using System.Threading.Tasks;
using CaixaEntity = API_Pdv.Entities.Caixa;

namespace API_Pdv.Infraestructure.Repositories;

public interface ICaixa
{
    Task<CaixaEntity> GetByIdAsync(int id);
    Task<IEnumerable<CaixaEntity>> GetAllAsync();
    Task<CaixaEntity> CreateAsync(CaixaEntity caixa);
    Task<CaixaEntity> UpdateAsync(CaixaEntity caixa);
    Task DeleteAsync(int id);
} 