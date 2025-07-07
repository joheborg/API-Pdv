using API_Pdv.Interfaces.Repositories;
using API_Pdv.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CategoriaEntities = API_Pdv.Entities.Categoria;

namespace WebPdv.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoria _categoriaRepository;

        public CategoriaController(ICategoria categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        // Development endpoint - no authentication required
        [HttpGet("dev/all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDev()
        {
            try
            {
                var categorias = await _categoriaRepository.GetAllAsync();
                return Ok(categorias);
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
                // Categorias são globais, não filtramos por empresa
                var categorias = await _categoriaRepository.GetAllAsync();
                return Ok(categorias);
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
                var categoria = await _categoriaRepository.GetByIdAsync(id);
                if (categoria == null)
                    return NotFound($"Categoria com ID {id} não encontrada");
                
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoriaEntities categoria)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdCategoria = await _categoriaRepository.CreateAsync(categoria);
                return CreatedAtAction(nameof(GetById), new { id = createdCategoria.Id }, createdCategoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoriaEntities categoria)
        {
            try
            {
                if (id != categoria.Id)
                    return BadRequest("ID da URL não corresponde ao ID da categoria");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedCategoria = await _categoriaRepository.UpdateAsync(categoria);
                return Ok(updatedCategoria);
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
                await _categoriaRepository.DeleteAsync(id);
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