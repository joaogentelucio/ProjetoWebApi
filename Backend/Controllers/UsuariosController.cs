using CadastroUsuarios.Models;
using CadastroUsuarios.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CadastroUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosRepository _usuariosRepository;

        public UsuariosController(UsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        // Criação de um novo usuário
        [HttpPost("CriarUsuario")]
        public IActionResult CriarUsuario([FromBody] Usuarios cadastro)
        {
            if (cadastro == null)
                return BadRequest("Dados do usuário inválidos.");

            try
            {
                _usuariosRepository.Salvar(cadastro);
                return CreatedAtAction(nameof(LerUsuarios), new { id = cadastro.Id }, cadastro);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar o usuário: {ex.Message}");
            }
        }

        // Ler todos os usuários
        [HttpGet("LerUsuarios")]
        public ActionResult<List<Usuarios>> LerUsuarios()
        {
            try
            {
                var usuarios = _usuariosRepository.LerUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter usuários: {ex.Message}");
            }
        }

        // Alterar os dados de um usuário
        [HttpPut("AlterarUsuario/{id}")]
        public IActionResult AlterarUsuario(int id, [FromBody] Usuarios usuarioAtualizado)
        {
            if (usuarioAtualizado == null)
                return BadRequest("Dados do usuário inválidos.");

            try
            {
                var sucesso = _usuariosRepository.AlterarUsuario(id, usuarioAtualizado);
                if (!sucesso)
                    return NotFound("Usuário não encontrado.");

                return Ok("Usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar o usuário: {ex.Message}");
            }
        }

        // Deletar um usuário pelo ID
        [HttpDelete("DeletarUsuario/{id}")]
        public IActionResult DeletarUsuario(int id)
        {
            try
            {
                var sucesso = _usuariosRepository.DeletarUsuario(id);
                if (!sucesso)
                    return NotFound("Usuário não encontrado.");

                return Ok("Usuário deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar o usuário: {ex.Message}");
            }
        }
    }
}
