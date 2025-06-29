using System.Collections.Generic;
using System.Threading.Tasks;
using MotoBoyEntity = API_Pdv.Entities.Motoboy;

namespace API_Pdv.Infraestructure.Repositories;
public interface IMotoboy
{
    Task<MotoBoyEntity> GetByIdAsync(int id);
    Task<IEnumerable<MotoBoyEntity>> GetAllAsync();
    Task<MotoBoyEntity> CreateAsync(MotoBoyEntity motoboy);
    Task<MotoBoyEntity> UpdateAsync(MotoBoyEntity motoboy);
    Task DeleteAsync(int id);
} 