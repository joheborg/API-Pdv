using API_Pdv.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using StatusPedidoEntities = API_Pdv.Entities.StatusPedido;

namespace WebPdv.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StatusPedidoController : ControllerBase
    {
        private readonly IStatusPedido _statusPedidoRepository;

        public StatusPedidoController(IStatusPedido statusPedidoRepository)
        {
            _statusPedidoRepository = statusPedidoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var status = await _statusPedidoRepository.GetAllAsync();
                return Ok(status);
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
                var status = await _statusPedidoRepository.GetByIdAsync(id);
                if (status == null)
                    return NotFound($"StatusPedido com ID {id} não encontrado");
                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatusPedidoEntities statusPedido)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var created = await _statusPedidoRepository.CreateAsync(statusPedido);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StatusPedidoEntities statusPedido)
        {
            try
            {
                if (id != statusPedido.Id)
                    return BadRequest("ID da URL não corresponde ao ID do status");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var updated = await _statusPedidoRepository.UpdateAsync(statusPedido);
                return Ok(updated);
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
                await _statusPedidoRepository.DeleteAsync(id);
                return NoContent();
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
    }
} 