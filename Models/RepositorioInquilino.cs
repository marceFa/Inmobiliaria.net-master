using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Inmobiliaria.Models
{
    public class RepositorioInquilino
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;

        public RepositorioInquilino(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public int Alta(Inquilino i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Inquilinos (Nombre, Apellido, Dni, Telefono, LugarDeTrabajo, NombreGarante, telefonoGarante) " +
                    $"VALUES (@nombre, @apellido, @dni, @telefono, @trabajo, @nombregarante, @telefonogarante);" +
                    $"SELECT SCOPE_IDENTITY();";//devuelve el id insertado
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", i.Nombre);
                    command.Parameters.AddWithValue("@apellido", i.Apellido);
                    command.Parameters.AddWithValue("@dni", i.Dni);
                    command.Parameters.AddWithValue("@telefono", i.Telefono);
                    command.Parameters.AddWithValue("@trabajo", i.LugarDeTrabajo);
                    command.Parameters.AddWithValue("@nombregarante", i.nombreGarante);
                    command.Parameters.AddWithValue("@telfonogarante", i.telefonoGarante);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    i.idInquilino = res;
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
                string sql = $"DELETE FROM Inquilinos WHERE idInquilino = @id";
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
        public int Modificacion(Inquilino i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inquilinos SET Nombre=@nombre, Apellido=@apellido, Dni=@dni, Telefono=@telefono, LugarDeTrabajo=@trabajo, NombreGarante=@nombregarante, telefonoGarante=@telefonogarante " +
                    $"WHERE idInquilino = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nombre", i.Nombre);
                    command.Parameters.AddWithValue("@apellido", i.Apellido);
                    command.Parameters.AddWithValue("@dni", i.Dni);
                    command.Parameters.AddWithValue("@telefono", i.Telefono);
                    command.Parameters.AddWithValue("@trabajo", i.LugarDeTrabajo);
                    command.Parameters.AddWithValue("@nombregarante", i.nombreGarante);
                    command.Parameters.AddWithValue("@telefonogarante", i.telefonoGarante);
                    command.Parameters.AddWithValue("@id", i.idInquilino);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Inquilino> ObtenerTodos()
        {
            IList<Inquilino> res = new List<Inquilino>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idInquilino, Nombre, Apellido, Dni, Telefono, LugarDeTrabajo, NombreGarante, telefonoGarante" +
                    $" FROM Inquilinos";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inquilino i = new Inquilino
                        {
                            idInquilino = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            LugarDeTrabajo = reader.GetString(5),
                            nombreGarante = reader.GetString(6),
                            telefonoGarante = reader.GetString(7),
                        };
                        res.Add(i);
                    }
                    connection.Close();
                }
            }
            return res;
        }
        public Inquilino ObtenerPorId(int id)
        {
            Inquilino i = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idInquilino, Nombre, Apellido, Dni, Telefono, LugarDeTrabajo, NombreGarante, telefonoGarante FROM Inquilinos" +
                    $" WHERE idInquilino=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        i = new Inquilino
                        {
                            idInquilino = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            LugarDeTrabajo = reader.GetString(5),
                            nombreGarante = reader.GetString(6),
                            telefonoGarante = reader.GetString(7),

                        };
                    }
                    connection.Close();
                }
            }
            return i;
        }
    }
}
