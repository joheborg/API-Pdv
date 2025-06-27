using EmpresaEntities = API_Pdv.Entities.Empresa;

namespace API_Pdv.Interfaces.Repositories;

public interface IEmpresa
{
    Task<EmpresaEntities> GetByIdAsync(int id);
    Task<IEnumerable<EmpresaEntities>> GetAllAsync();
    Task<EmpresaEntities> CreateAsync(EmpresaEntities empresa);
    Task<EmpresaEntities> UpdateAsync(EmpresaEntities empresa);
    Task DeleteAsync(int id);
    Task<EmpresaEntities> GetByCnpjAsync(string cnpj);
}