using Microsoft.AspNetCore.Mvc;

namespace WebPdv.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Lista todos os usuários");
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Usuário com ID {id}");
        }
        
        [HttpPost]
        public IActionResult Create()
        {
            return Ok("Criar usuário");
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id)
        {
            return Ok($"Atualizar usuário {id}");
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deletar usuário {id}");
        }
    }
}