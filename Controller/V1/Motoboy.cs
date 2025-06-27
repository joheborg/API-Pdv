using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MotoBoyEntity = API_Pdv.Entities.Motoboy;


[ApiController]
[Route("api/v1/[controller]")]
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
        var motoboys = await _motoboyRepository.GetAllAsync();
        return Ok(motoboys);
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
        var created = await _motoboyRepository.CreateAsync(motoboy);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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