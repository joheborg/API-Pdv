using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ItemPedidoEntity = API_Pdv.Entities.ItemPedido;


[ApiController]
[Route("api/v1/[controller]")]
public class ItemPedidoController : ControllerBase
{
    private readonly IItemPedido _itemPedidoRepository;

    public ItemPedidoController(IItemPedido itemPedidoRepository)
    {
        _itemPedidoRepository = itemPedidoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var itens = await _itemPedidoRepository.GetAllAsync();
        return Ok(itens);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _itemPedidoRepository.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ItemPedidoEntity item)
    {
        var created = await _itemPedidoRepository.CreateAsync(item);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ItemPedidoEntity item)
    {
        if (id != item.Id)
            return BadRequest();
        var updated = await _itemPedidoRepository.UpdateAsync(item);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _itemPedidoRepository.DeleteAsync(id);
        return NoContent();
    }
} 