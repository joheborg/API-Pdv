using API_Pdv.Interfaces.Repositories;
using API_Pdv.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AvaliacaoEntities = API_Pdv.Entities.Avaliacao;

namespace WebPdv.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [AllowAnonymous] // Permitir acesso sem autenticação para avaliações
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacao _avaliacaoRepository;
        private readonly IPedido _pedidoRepository;

        public AvaliacaoController(IAvaliacao avaliacaoRepository, IPedido pedidoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _pedidoRepository = pedidoRepository;
        }

        // Rota principal para criar avaliação
        [HttpPost("avaliar")]
        public async Task<IActionResult> Avaliar([FromBody] AvaliacaoEntities avaliacao)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Validar se a nota está entre 1 e 5
                if (avaliacao.Nota < 1 || avaliacao.Nota > 5)
                {
                    return BadRequest("A nota deve estar entre 1 e 5");
                }

                // Verificar se já existe uma avaliação para esta comanda
                var empresaId = UserHelper.GetCurrentUserEmpresaId(HttpContext) ?? 1; // Fallback para desenvolvimento
                var avaliacaoExistente = await _avaliacaoRepository.GetByNumeroComandaAsync(avaliacao.NumeroComanda, empresaId);
                
                if (avaliacaoExistente != null)
                {
                    return BadRequest($"Já existe uma avaliação para a comanda {avaliacao.NumeroComanda}");
                }

                // Verificar se a comanda existe
                var pedido = await _pedidoRepository.GetByNumeroComandaAsync(avaliacao.NumeroComanda, empresaId);
                if (pedido == null)
                {
                    return NotFound($"Comanda {avaliacao.NumeroComanda} não encontrada");
                }

                // Definir empresa ID
                avaliacao.EmpresaId = empresaId;
                
                var createdAvaliacao = await _avaliacaoRepository.CreateAsync(avaliacao);
                return CreatedAtAction(nameof(GetById), new { id = createdAvaliacao.Id }, createdAvaliacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        // Development endpoint - no authentication required
        [HttpGet("dev/all")]
        public async Task<IActionResult> GetAllDev()
        {
            try
            {
                var avaliacoes = await _avaliacaoRepository.GetAllAsync();
                return Ok(avaliacoes);
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

                var avaliacoes = await _avaliacaoRepository.GetByEmpresaAsync(empresaId.Value);
                return Ok(avaliacoes);
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
                var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);
                if (avaliacao == null)
                    return NotFound($"Avaliação com ID {id} não encontrada");
                
                return Ok(avaliacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
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

                var avaliacao = await _avaliacaoRepository.GetByNumeroComandaAsync(numeroComanda, empresaId.Value);
                if (avaliacao == null)
                    return NotFound($"Avaliação para comanda {numeroComanda} não encontrada");
                
                return Ok(avaliacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AvaliacaoEntities avaliacao)
        {
            try
            {
                if (id != avaliacao.Id)
                    return BadRequest("ID da URL não corresponde ao ID da avaliação");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Validar se a nota está entre 1 e 5
                if (avaliacao.Nota < 1 || avaliacao.Nota > 5)
                {
                    return BadRequest("A nota deve estar entre 1 e 5");
                }

                var updatedAvaliacao = await _avaliacaoRepository.UpdateAsync(avaliacao);
                return Ok(updatedAvaliacao);
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
                await _avaliacaoRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
} 