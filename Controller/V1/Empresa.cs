using API_Pdv.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmpresaEntities = API_Pdv.Entities.Empresa;

namespace WebPdv.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresa _empresaRepository;

        public EmpresaController(IEmpresa empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var empresas = await _empresaRepository.GetAllAsync();
                return Ok(empresas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var empresa = await _empresaRepository.GetByIdAsync(id);
                if (empresa == null)
                    return NotFound($"Empresa com ID {id} não encontrada");
                
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("cnpj/{cnpj}")]
        public async Task<IActionResult> GetByCnpj(string cnpj)
        {
            try
            {
                var empresa = await _empresaRepository.GetByCnpjAsync(cnpj);
                if (empresa == null)
                    return NotFound($"Empresa com CNPJ {cnpj} não encontrada");
                
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmpresaEntities empresa)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdEmpresa = await _empresaRepository.CreateAsync(empresa);
                return CreatedAtAction(nameof(GetById), new { id = createdEmpresa.Id }, createdEmpresa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmpresaEntities empresa)
        {
            try
            {
                if (id != empresa.Id)
                    return BadRequest("ID da URL não corresponde ao ID da empresa");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedEmpresa = await _empresaRepository.UpdateAsync(empresa);
                return Ok(updatedEmpresa);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _empresaRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}