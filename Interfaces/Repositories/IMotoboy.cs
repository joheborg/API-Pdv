using System.Collections.Generic;
using System.Threading.Tasks;
using MotoBoyEntity = API_Pdv.Entities.Motoboy;
public interface IMotoboy
{
    Task<MotoBoyEntity> GetByIdAsync(int id);
    Task<IEnumerable<MotoBoyEntity>> GetAllAsync();
    Task<MotoBoyEntity> CreateAsync(MotoBoyEntity motoboy);
    Task<MotoBoyEntity> UpdateAsync(MotoBoyEntity motoboy);
    Task DeleteAsync(int id);
} 