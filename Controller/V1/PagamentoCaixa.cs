using API_Pdv.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PagamentoCaixaEntity = API_Pdv.Entities.PagamentoCaixa;

[ApiController]
[Route("api/v1/[controller]")]
public class PagamentoCaixaController : ControllerBase
{
    private readonly IPagamentoCaixa _pagamentoCaixaRepository;

    public PagamentoCaixaController(IPagamentoCaixa pagamentoCaixaRepository)
    {
        _pagamentoCaixaRepository = pagamentoCaixaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pagamentos = await _pagamentoCaixaRepository.GetAllAsync();
        return Ok(pagamentos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pagamento = await _pagamentoCaixaRepository.GetByIdAsync(id);
        if (pagamento == null)
            return NotFound();
        return Ok(pagamento);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PagamentoCaixaEntity pagamento)
    {
        var created = await _pagamentoCaixaRepository.CreateAsync(pagamento);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PagamentoCaixaEntity pagamento)
    {
        if (id != pagamento.Id)
            return BadRequest();
        var updated = await _pagamentoCaixaRepository.UpdateAsync(pagamento);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _pagamentoCaixaRepository.DeleteAsync(id);
        return NoContent();
    }
} 