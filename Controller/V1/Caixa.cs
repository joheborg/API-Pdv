using API_Pdv.Infraestructure.Repositories;
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
        var caixas = await _caixaRepository.GetAllAsync();
        return Ok(caixas);
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
        var created = await _caixaRepository.CreateAsync(caixa);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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