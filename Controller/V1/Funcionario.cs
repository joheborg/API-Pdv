using Microsoft.AspNetCore.Mvc;

namespace WebPdv.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Lista todos os funcionários");
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Funcionário com ID {id}");
        }
        
        [HttpPost]
        public IActionResult Create()
        {
            return Ok("Criar funcionário");
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id)
        {
            return Ok($"Atualizar funcionário {id}");
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deletar funcionário {id}");
        }
    }
}