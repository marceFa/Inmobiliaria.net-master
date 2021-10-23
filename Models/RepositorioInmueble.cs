using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inmobiliaria.Models
{
    public class RepositorioInmueble
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;
        public RepositorioInmueble(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public int Alta(Inmueble i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Inmuebles (Direccion, Ambientes, Tipo, Uso, Precio, id, Disponible) " +
                    "VALUES (@direccion, @ambientes, @tipo, @uso, @precio, @id, @disponible);" +
                    "SELECT SCOPE_IDENTITY();";//devuelve el id insertado (LAST_INSERT_ID para mysql)
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@direccion", i.Direccion);
                    command.Parameters.AddWithValue("@ambientes", i.Ambientes);
                    command.Parameters.AddWithValue("@precio", i.Precio);
                    command.Parameters.AddWithValue("@tipo", i.Tipo);
                    command.Parameters.AddWithValue("@uso", i.Uso);
                    command.Parameters.AddWithValue("@id", i.id);
                    command.Parameters.AddWithValue("@disponible", i.Disponible);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    i.idInmueble = res;
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
                string sql = $"DELETE FROM Inmuebles WHERE idInmueble = {id}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public int Modificacion(Inmueble i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Inmuebles SET " +
                    "Direccion=@direccion, Ambientes=@ambientes, Uso=@uso, Disponible=@disponible, Tipo=@tipo, Precio=@precio, id=@id " +
                    "WHERE idInmueble = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@direccion", i.Direccion);
                    command.Parameters.AddWithValue("@ambientes", i.Ambientes);
                    command.Parameters.AddWithValue("@disponible", i.Disponible);
                    command.Parameters.AddWithValue("@precio", i.Precio);
                    command.Parameters.AddWithValue("@uso", i.Uso);
                    command.Parameters.AddWithValue("@id", i.id);
                    command.Parameters.AddWithValue("@tipo", i.Tipo);
                    command.Parameters.AddWithValue("@id", i.idInmueble);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int NoDisponible(Inmueble inm)
        {
            int i = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inmuebles SET Disponible= {0} " +
                    $"WHERE idInmueble = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    //command.Parameters.AddWithValue("@disponible", inm.Disponible);
                    command.Parameters.AddWithValue("@id", inm.idInmueble);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        }

        public int Disponible(Inmueble inm)
        {
            int i = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inmuebles SET Disponible= {1} " +
                    $"WHERE idInmueble = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    //command.Parameters.AddWithValue("@disponible", inm.Disponible);
                    command.Parameters.AddWithValue("@id", inm.idInmueble);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        }


        public IList<Inmueble> ObtenerTodos()
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idInmueble, Direccion, Ambientes, Tipo, Uso, Precio, i.id, Disponible," +
                    $" p.Nombre, p.Apellido" +
                    $" FROM Inmuebles i INNER JOIN Propietarios p ON i.id = p.id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inmueble inm = new Inmueble
                        {
                            idInmueble = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Tipo = reader.GetString(3),
                            Uso = reader.GetString(4),
                            Precio = reader.GetDecimal(5),
                            id = reader.GetInt32(6),
                            Disponible = reader.GetBoolean(7),
                            Propietarios = new Propietario
                            {
                                //id = reader.GetInt32(8),
                                Nombre = reader.GetString(8),
                                Apellido = reader.GetString(9),
                            }
                        };
                        res.Add(inm);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public Inmueble ObtenerPorId(int id)
        {
            Inmueble inm = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idInmueble, Direccion, Ambientes, Tipo, Uso, Precio, i.id, Disponible, p.Nombre, p.Apellido" +
                    $" FROM Inmuebles i INNER JOIN Propietarios p ON i.id = p.id" +
                    $" WHERE idInmueble=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        inm = new Inmueble
                        {
                            idInmueble = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Tipo = reader.GetString(3),
                            Uso = reader.GetString(4),
                            Precio = reader.GetDecimal(5),
                            id = reader.GetInt32(6),
                            Disponible = reader.GetBoolean(7),
                            Propietarios = new Propietario
                            {
                                //id = reader.GetInt32(8),
                                Nombre = reader.GetString(8),
                                Apellido = reader.GetString(9),
                            }
                        };
                    }
                    connection.Close();
                }
            }
            return inm;
        }

        public IList<Inmueble> ObtenerSiDisponible()
        {
            IList<Inmueble> inm = new List<Inmueble>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idInmueble, Direccion, Ambientes, Tipo, Uso, Precio, i.id, Disponible, p.Nombre, p.Apellido" +
                    $" FROM Inmuebles i INNER JOIN Propietarios p ON i.id = p.id" +
                    $" WHERE Disponible= {1} ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inmueble i = new Inmueble
                        {
                            idInmueble = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Tipo = reader.GetString(3),
                            Uso = reader.GetString(4),
                            Precio = reader.GetDecimal(5),
                            id = reader.GetInt32(6),
                            Disponible = reader.GetBoolean(7),
                            Propietarios = new Propietario
                            {
                                Nombre = reader.GetString(8),
                                Apellido = reader.GetString(9),
                            }
                        };
                        inm.Add(i);
                    }
                    connection.Close();
                }
            }
            return inm;
        }
        public IList<Inmueble> BuscarPorPropietario(int id)
        {
            List<Inmueble> res = new List<Inmueble>();
            Inmueble inm = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idInmueble, Direccion, Ambientes, Tipo, Uso, Precio, i.id, Disponible, p.Nombre, p.Apellido" +
                    $" FROM Inmuebles i INNER JOIN Propietarios p ON i.id = p.id" +
                    $" WHERE i.id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        inm = new Inmueble
                        {
                            idInmueble = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Tipo = reader.GetString(3),
                            Uso = reader.GetString(4),
                            Precio = reader.GetDecimal(5),
                            id = reader.GetInt32(6),
                            Disponible = reader.GetBoolean(7),
                            Propietarios = new Propietario
                            {
                                Nombre = reader.GetString(8),
                                Apellido = reader.GetString(9)
                            }
                        };
                        res.Add(inm);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Inmueble> BuscarDesocupados(DateTime i, DateTime f)
        {
            List<Inmueble> res = new List<Inmueble>();
            Inmueble inm = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT i.idInmueble, Direccion, Ambientes, Tipo, Uso, Precio, i.id, Disponible, " +
                    $"p.Nombre, p.Apellido " +
                    $" FROM Inmuebles i INNER JOIN Contratos c ON i.idInmueble = c.idInmueble  INNER JOIN Propietarios p ON i.id = p.id " +
                    $" WHERE ( @fechaInicio > FechaFin) OR (FechaInicio > @fechaFin) " +
                    $"UNION SELECT i.idInmueble, Direccion, Ambientes, Tipo, Uso, Precio, i.id, Disponible, p.Nombre, p.Apellido FROM Inmuebles i INNER JOIN Propietarios p ON i.id = p.id WHERE i.idInmueble NOT IN (SELECT i.idInmueble FROM Inmuebles i INNER JOIN Contratos c ON i.idInmueble = c.idInmueble) ";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@fechaFin", SqlDbType.DateTime).Value = f;
                    command.Parameters.Add("@fechaInicio", SqlDbType.DateTime).Value = i;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        inm = new Inmueble
                        {
                            idInmueble = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Tipo = reader.GetString(3),
                            Uso = reader.GetString(4),
                            Precio = reader.GetDecimal(5),
                            id = reader.GetInt32(6),
                            Disponible = reader.GetBoolean(7),
                            Propietarios = new Propietario
                            {
                                Nombre = reader.GetString(8),
                                Apellido = reader.GetString(9)
                            }

                        };
                        res.Add(inm);
                    }
                    connection.Close();
                }
            }
            return res;
        }
    }
}
