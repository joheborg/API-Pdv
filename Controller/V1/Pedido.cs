using API_Pdv.Interfaces.Repositories;
using API_Pdv.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using PedidoEntity = API_Pdv.Entities.Pedido;

namespace WebPdv.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class PedidoController : ControllerBase
    {
        private readonly IPedido _pedidoRepository;

        public PedidoController(IPedido pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        // Development endpoint - no authentication required
        [HttpGet("dev/all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDev()
        {
            try
            {
                var pedidos = await _pedidoRepository.GetAllAsync();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("dev/empresa/{empresaId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByEmpresaDev(int empresaId)
        {
            try
            {
                var pedidos = await _pedidoRepository.GetByEmpresaAsync(empresaId);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
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

                var pedidos = await _pedidoRepository.GetByEmpresaAsync(empresaId.Value);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null)
                return NotFound();
            return Ok(pedido);
        }

        [HttpGet("comanda/{numeroComanda}")]
        public async Task<IActionResult> GetByNumeroComanda(string numeroComanda)
        {
            try
            {
                var empresaId = UserHelper.GetCurrentUserEmpresaId(HttpContext);
                if (!empresaId.HasValue)
                {
                    return BadRequest("Usuário não possui empresa associada");
                }

                var pedido = await _pedidoRepository.GetByNumeroComandaAsync(numeroComanda, empresaId.Value);
                if (pedido == null)
                    return NotFound($"Pedido com comanda {numeroComanda} não encontrado");
                
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("abertos")]
        public async Task<IActionResult> GetPedidosAbertos()
        {
            try
            {
                var empresaId = UserHelper.GetCurrentUserEmpresaId(HttpContext);
                if (!empresaId.HasValue)
                {
                    return BadRequest("Usuário não possui empresa associada");
                }

                var pedidos = await _pedidoRepository.GetPedidosAbertosAsync(empresaId.Value);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PedidoEntity pedido)
        {
            try
            {
                var empresaId = UserHelper.GetCurrentUserEmpresaId(HttpContext);
                if (!empresaId.HasValue)
                {
                    return BadRequest("Usuário não possui empresa associada");
                }

                pedido.EmpresaId = empresaId.Value;
                var created = await _pedidoRepository.CreateAsync(pedido);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
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
} 