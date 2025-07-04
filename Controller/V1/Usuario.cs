using API_Pdv.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using UsuarioEntities = API_Pdv.Entities.Usuario;

namespace WebPdv.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioRepository;

        public UsuarioController(IUsuario usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                return Ok(usuarios);
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
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null)
                    return NotFound($"Usuário com ID {id} não encontrado");
                
                return Ok(usuario);
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
                var usuarios = await _usuarioRepository.GetByEmpresaAsync(empresaId);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("ativos")]
        public async Task<IActionResult> GetAtivos()
        {
            try
            {
                var usuarios = await _usuarioRepository.GetAtivosAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByEmailAsync(email);
                if (usuario == null)
                    return NotFound($"Usuário com email {email} não encontrado");
                
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioEntities usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Verificar se já existe um usuário com o mesmo email
                var existingUser = await _usuarioRepository.GetByEmailAsync(usuario.Email);
                if (existingUser != null)
                    return BadRequest($"Já existe um usuário com o email {usuario.Email}");

                var createdUsuario = await _usuarioRepository.CreateAsync(usuario);
                return CreatedAtAction(nameof(GetById), new { id = createdUsuario.Id }, createdUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioEntities usuario)
        {
            try
            {
                if (id != usuario.Id)
                    return BadRequest("ID da URL não corresponde ao ID do usuário");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Verificar se existe outro usuário com o mesmo email (exceto o atual)
                var existingUser = await _usuarioRepository.GetByEmailAsync(usuario.Email);
                if (existingUser != null && existingUser.Id != id)
                    return BadRequest($"Já existe outro usuário com o email {usuario.Email}");

                var updatedUsuario = await _usuarioRepository.UpdateAsync(usuario);
                return Ok(updatedUsuario);
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
                await _usuarioRepository.DeleteAsync(id);
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

        [HttpPut("{id}/ativar")]
        public async Task<IActionResult> Ativar(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null)
                    return NotFound($"Usuário com ID {id} não encontrado");

                usuario.Ativo = true;
                usuario.UpdatedAt = DateTime.Now;

                var updatedUsuario = await _usuarioRepository.UpdateAsync(usuario);
                return Ok(updatedUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}/desativar")]
        public async Task<IActionResult> Desativar(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null)
                    return NotFound($"Usuário com ID {id} não encontrado");

                usuario.Ativo = false;
                usuario.UpdatedAt = DateTime.Now;

                var updatedUsuario = await _usuarioRepository.UpdateAsync(usuario);
                return Ok(updatedUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}/alterar-senha")]
        public async Task<IActionResult> AlterarSenha(int id, [FromBody] AlterarSenhaRequest request)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null)
                    return NotFound($"Usuário com ID {id} não encontrado");

                // Aqui você pode adicionar validação da senha atual se necessário
                usuario.Senha = request.NovaSenha;
                usuario.UpdatedAt = DateTime.Now;

                var updatedUsuario = await _usuarioRepository.UpdateAsync(usuario);
                return Ok(new { message = "Senha alterada com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }

    public class AlterarSenhaRequest
    {
        public string NovaSenha { get; set; } = "";
    }
}