using FirebirdSql.Data.FirebirdClient;
using CadastroUsuarios.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CadastroUsuarios.Models.Repository
{
    public class UsuariosRepository
    {
        private readonly string _connectionString;

        // O IConfiguration é injetado aqui automaticamente
        public UsuariosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("FirebirdConnection");
        }

        // Métodos para salvar, ler, alterar e deletar usuários
        public void Salvar(Usuarios usuarios)
        {
            using (var connection = new FbConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO usuarios (nome, email, senha) VALUES (@Nome, @Email, @Senha)";
                using (var command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", usuarios.Nome);
                    command.Parameters.AddWithValue("@Email", usuarios.Email);
                    command.Parameters.AddWithValue("@Senha", usuarios.Senha);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Usuarios> LerUsuarios()
        {
            var usuariosList = new List<Usuarios>();

            using (var connection = new FbConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT id, nome, email, senha FROM usuarios";
                using (var command = new FbCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuariosList.Add(new Usuarios
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Email = reader.GetString(2),
                            Senha = reader.GetString(3)
                        });
                    }
                }
            }

            return usuariosList;
        }

        public bool AlterarUsuario(int id, Usuarios usuarioAtualizado)
        {
            using (var connection = new FbConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE usuarios SET nome = @Nome, email = @Email, senha = @Senha WHERE id = @Id";
                using (var command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", usuarioAtualizado.Nome);
                    command.Parameters.AddWithValue("@Email", usuarioAtualizado.Email);
                    command.Parameters.AddWithValue("@Senha", usuarioAtualizado.Senha);
                    command.Parameters.AddWithValue("@Id", id);

                    var rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool DeletarUsuario(int id)
        {
            using (var connection = new FbConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM usuarios WHERE id = @Id";
                using (var command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    var rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
