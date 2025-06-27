using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using PedidoEntity = API_Pdv.Entities.Pedido;


[ApiController]
[Route("api/v1/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedido _pedidoRepository;

    public PedidoController(IPedido pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pedidos = await _pedidoRepository.GetAllAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);
        if (pedido == null)
            return NotFound();
        return Ok(pedido);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PedidoEntity pedido)
    {
        var created = await _pedidoRepository.CreateAsync(pedido);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PedidoEntity pedido)
    {
        if (id != pedido.Id)
            return BadRequest();
        var updated = await _pedidoRepository.UpdateAsync(pedido);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _pedidoRepository.DeleteAsync(id);
        return NoContent();
    }
} 