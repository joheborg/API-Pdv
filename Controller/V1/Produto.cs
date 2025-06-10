using Microsoft.AspNetCore.Mvc;

namespace WebPdv.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Olá Mundo da Controller");
        }
        
        [HttpGet]
        public IActionResult GetById(int id)
        {
            return Ok("Olá Mundo da Controller");
        }
        
        [HttpPost]
        public IActionResult Post(Produto produto)
        {
            return Ok("Olá Mundo da Controller"+produto.Descricao);
        }
        
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok("Olá Mundo da Controller");
        }
    }
}