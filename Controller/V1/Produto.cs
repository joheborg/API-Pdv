using API_Pdv.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using ProdutoEntities = API_Pdv.Entities.Produto;

namespace WebPdv.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProduto _produtoRepository;

        public ProdutoController(IProduto produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var produtos = await _produtoRepository.GetAllAsync();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("empresa/{empresaId}")]
        public async Task<IActionResult> GetByEmpresa(int empresaId)
        {
            try
            {
                var produtos = await _produtoRepository.GetByEmpresaAsync(empresaId);
                return Ok(produtos);
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
                var produto = await _produtoRepository.GetByIdAsync(id);
                if (produto == null)
                    return NotFound($"Produto com ID {id} não encontrado");
                
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("codigo/{empresaId}/{codigoProduto}")]
        public async Task<IActionResult> GetByCodigo(int empresaId, string codigoProduto)
        {
            try
            {
                var produto = await _produtoRepository.GetByCodigoAsync(empresaId, codigoProduto);
                if (produto == null)
                    return NotFound($"Produto com código {codigoProduto} não encontrado na empresa {empresaId}");
                
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("barras/{codigoBarras}")]
        public async Task<IActionResult> GetByCodigoBarras(string codigoBarras)
        {
            try
            {
                var produto = await _produtoRepository.GetByCodigoBarrasAsync(codigoBarras);
                if (produto == null)
                    return NotFound($"Produto com código de barras {codigoBarras} não encontrado");
                
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("ean/{codigoEan}")]
        public async Task<IActionResult> GetByCodigoEan(string codigoEan)
        {
            try
            {
                var produto = await _produtoRepository.GetByCodigoEanAsync(codigoEan);
                if (produto == null)
                    return NotFound($"Produto com código EAN {codigoEan} não encontrado");
                
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<IActionResult> GetByCategoria(int categoriaId)
        {
            try
            {
                var produtos = await _produtoRepository.GetByCategoriaAsync(categoriaId);
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProdutoEntities produto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdProduto = await _produtoRepository.CreateAsync(produto);
                return CreatedAtAction(nameof(GetById), new { id = createdProduto.Id }, createdProduto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProdutoEntities produto)
        {
            try
            {
                if (id != produto.Id)
                    return BadRequest("ID da URL não corresponde ao ID do produto");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedProduto = await _produtoRepository.UpdateAsync(produto);
                return Ok(updatedProduto);
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
                await _produtoRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}