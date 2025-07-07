using API_Pdv.Infraestructure.Repositories;
using API_Pdv.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CaixaEntity = API_Pdv.Entities.Caixa;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class CaixaController : ControllerBase
{
    private readonly ICaixa _caixaRepository;

    public CaixaController(ICaixa caixaRepository)
    {
        _caixaRepository = caixaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var empresaId = UserHelper.GetCurrentUserEmpresaId(HttpContext);
            if (!empresaId.HasValue)
            {
                return BadRequest("Usuário não possui empresa associada");
            }

            var caixas = await _caixaRepository.GetByEmpresaAsync(empresaId.Value);
            return Ok(caixas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var caixa = await _caixaRepository.GetByIdAsync(id);
        if (caixa == null)
            return NotFound();
        return Ok(caixa);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CaixaEntity caixa)
    {
        try
        {
            var empresaId = UserHelper.GetCurrentUserEmpresaId(HttpContext);
            if (!empresaId.HasValue)
            {
                return BadRequest("Usuário não possui empresa associada");
            }

            caixa.EmpresaId = empresaId.Value;
            var created = await _caixaRepository.CreateAsync(caixa);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CaixaEntity caixa)
    {
        if (id != caixa.Id)
            return BadRequest();
        var updated = await _caixaRepository.UpdateAsync(caixa);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _caixaRepository.DeleteAsync(id);
        return NoContent();
    }
} 