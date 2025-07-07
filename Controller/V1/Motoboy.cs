using API_Pdv.Infraestructure.Repositories;
using API_Pdv.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MotoBoyEntity = API_Pdv.Entities.Motoboy;


[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class MotoboyController : ControllerBase
{
    private readonly IMotoboy _motoboyRepository;

    public MotoboyController(IMotoboy motoboyRepository)
    {
        _motoboyRepository = motoboyRepository;
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

            var motoboys = await _motoboyRepository.GetByEmpresaAsync(empresaId.Value);
            return Ok(motoboys);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var motoboy = await _motoboyRepository.GetByIdAsync(id);
        if (motoboy == null)
            return NotFound();
        return Ok(motoboy);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MotoBoyEntity motoboy)
    {
        try
        {
            var empresaId = UserHelper.GetCurrentUserEmpresaId(HttpContext);
            if (!empresaId.HasValue)
            {
                return BadRequest("Usuário não possui empresa associada");
            }

            motoboy.EmpresaId = empresaId.Value;
            var created = await _motoboyRepository.CreateAsync(motoboy);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MotoBoyEntity motoboy)
    {
        if (id != motoboy.Id)
            return BadRequest();
        var updated = await _motoboyRepository.UpdateAsync(motoboy);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _motoboyRepository.DeleteAsync(id);
        return NoContent();
    }
} 