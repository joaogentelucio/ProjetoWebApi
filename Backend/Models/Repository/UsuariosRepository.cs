using Newtonsoft.Json;

namespace CadastroUsuarios.Models.Repository
{
    public class UsuariosRepository
    {
        public void Salvar(Usuarios usuarios)
        {
            var usuariosTexto = JsonConvert.SerializeObject(usuarios) + "," + Environment.NewLine;
            File.AppendAllText("C:\\Users\\JoaoVitor\\Documents\\GitHub\\Application\\Backend\\DataBase\\MasterDB.txt", usuariosTexto);
        }
        public List<Usuarios> LerUsuarios()
        {
            var usuarios = File.ReadAllText("C:\\Users\\JoaoVitor\\Documents\\GitHub\\Application\\Backend\\DataBase\\MasterDB.txt");
            List<Usuarios> UsuariosLista = JsonConvert.DeserializeObject<List<Usuarios>>("[" + usuarios + "]");

            return UsuariosLista.OrderByDescending(t => t.Nome).ToList();

        }

        public Usuarios AlterarUsuario(int Id)
        {
            var usuarioLista = LerUsuarios();
            var item = usuarioLista.Where(t => t.Id == Id).FirstOrDefault();

            return item;

        }

    }
}
