using CadastroUsuarios.Models;
using CadastroUsuarios.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadastroClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpPost("CriarUsuario")]
        public object CriarUsuario([FromBody] Usuarios cadastro)
        {
            try
            {
                UsuariosRepository usuarios = new UsuariosRepository();
                usuarios.Salvar(cadastro);
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        [HttpGet("LerUsuarios")]
        public object LerUsuarios()
        {
            List<Usuarios> listarUsuarios = null;
            try
            { 
                UsuariosRepository usuariosRepo = new UsuariosRepository();
                listarUsuarios = usuariosRepo.LerUsuarios();
            }
            catch (Exception ex)
            {

            }

            return listarUsuarios;
        }

        [HttpPut("Alterar")]
        public object Alterar(int Id)
        {
            try
            {
                UsuariosRepository usuarios = new UsuariosRepository();
                usuarios.AlterarUsuario(Id);
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        

        [HttpDelete("DeletarUsuario")]
        public object DeletarUsuario(int Id)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
