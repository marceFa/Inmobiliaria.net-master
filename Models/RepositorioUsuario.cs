using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Inmobiliaria.Models
{
    public class RepositorioUsuario
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;

        public RepositorioUsuario(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public int Alta(Usuario i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Usuarios (Nombre, Apellido, Email, Clave, Rol, Avatar) " +
                    $"VALUES (@nombre, @apellido, @email, @clave, @rol, @avatar);" +
                    $"SELECT SCOPE_IDENTITY();";//devuelve el id insertado
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", i.Nombre);
                    command.Parameters.AddWithValue("@apellido", i.Apellido);
                    command.Parameters.AddWithValue("@email", i.Email);
                    command.Parameters.AddWithValue("@clave", i.Clave);
                    command.Parameters.AddWithValue("@rol", i.Rol);
                    command.Parameters.AddWithValue("@avatar", i.Avatar);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    i.IdUs = res;
                    connection.Close();
                }
            }
            return res;
        }
        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Usuarios WHERE IdUs = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public int Modificacion(Usuario i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Usuarios SET Nombre=@nombre, Apellido=@apellido, Email=@email, Clave=@clave, Rol=@rol, Avatar=@avatar " +
                    $"WHERE IdUs = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", i.Nombre);
                    command.Parameters.AddWithValue("@apellido", i.Apellido);
                    command.Parameters.AddWithValue("@email", i.Email);
                    command.Parameters.AddWithValue("@clave", i.Clave);
                    command.Parameters.AddWithValue("@rol", i.Rol);
                    command.Parameters.AddWithValue("@avatar", i.Avatar);
                    command.Parameters.AddWithValue("@id", i.IdUs);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }


        public IList<Usuario> ObtenerTodos()
        {
            IList<Usuario> res = new List<Usuario>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT IdUs, Nombre, Apellido, Email, Clave, Rol, Avatar" +
                    $" FROM Usuarios";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Usuario p = new Usuario
                        {
                            IdUs = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Email = reader.GetString(3),
                            Clave = reader.GetString(4),
                            Rol = reader.GetString(5),
                            Avatar = reader["Avatar"].ToString(),
                        };
                        res.Add(p);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public Usuario ObtenerPorId(int id)
        {
            Usuario i = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT IdUs, Nombre, Apellido, Email, Clave, Rol, Avatar FROM Usuarios" +
                    $" WHERE IdUs=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        i = new Usuario
                        {
                            IdUs = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Email = reader.GetString(3),
                            Clave = reader.GetString(4),
                            Rol = reader.GetString(5),
                            Avatar = reader["Avatar"].ToString(),
                        };
                        return i;
                    }
                    connection.Close();
                }
            }
            return i;
        }
        public Usuario ObtenerPorEmail(string email)
        {
            Usuario e = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT IdUs, Nombre, Apellido, Email, Clave, Rol, Avatar FROM Usuarios" +
                    $" WHERE Email=@email";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        e = new Usuario
                        {
                            IdUs = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Email = reader.GetString(3),
                            Clave = reader.GetString(4),
                            Rol = reader.GetString(5),
                            Avatar = reader["Avatar"].ToString(),
                        };
                        return e;
                    }
                    connection.Close();
                }
            }
            return e;
        }
    }
}


