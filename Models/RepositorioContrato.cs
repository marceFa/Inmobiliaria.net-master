using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inmobiliaria.Models
{
    public class RepositorioContrato
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;
        public RepositorioContrato(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public int Alta(Contrato c)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Contratos(idInmueble, idInquilino, FechaInicio, FechaFin, MontoAlquiler, Vigente)  " +
                    $"VALUES(@idInmueble, @idInquilino, @fechaI, @fechaF, @montoAlquiler, @vigente)" +
                    $"SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@idInmueble", c.idInmueble);
                    command.Parameters.AddWithValue("@idInquilino", c.idInquilino);
                    command.Parameters.AddWithValue("@fechaI", c.FechaInicio);
                    command.Parameters.AddWithValue("@fechaF", c.FechaFin);
                    command.Parameters.AddWithValue("@montoAlquiler", c.MontoAlquiler);
                    command.Parameters.AddWithValue("@vigente", c.Vigente);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    c.idContrato = res;
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
                string sql = $"DELETE FROM Contratos WHERE idContrato=@id";

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

        public int Modificacion(Contrato c)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Contratos SET idInmueble=@idInmueble, idInquilino=@idInquilino, FechaInicio=@fechaI, FechaFin=@fechaF, MontoAlquiler=@montoAlquiler, Vigente=@vigente " +
                        $"WHERE idContrato=@id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@idInmueble", c.idInmueble);
                    command.Parameters.AddWithValue("@idInquilino", c.idInquilino);
                    command.Parameters.AddWithValue("@fechaI", c.FechaInicio);
                    command.Parameters.AddWithValue("@fechaF", c.FechaFin);
                    command.Parameters.AddWithValue("@montoAlquiler", c.MontoAlquiler);
                    command.Parameters.AddWithValue("@vigente", c.Vigente);
                    command.Parameters.AddWithValue("@id", c.idContrato);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return res;
        }

        public int NoVigente(Contrato con)
        {
            int i = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Contratos SET Vigente= {0} " +
                    $"WHERE idContrato = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", con.idContrato);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        }

        public int Vigente(Contrato con)
        {
            int i = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Contratos SET Vigente= {1} " +
                   $"WHERE idContrato = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", con.idContrato);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    i = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return i;
        }

        public IList<Contrato> ObtenerTodos()
        {
            IList<Contrato> res = new List<Contrato>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idContrato, c.idInquilino, c.idInmueble, FechaInicio, FechaFin, MontoAlquiler, Vigente," +
                    $" inq.Nombre, inq.Apellido," +
                    $" inm.Direccion" +
                    $" FROM Contratos c INNER JOIN Inmuebles inm ON c.idInmueble = inm.idInmueble INNER JOIN Inquilinos inq ON c.idInquilino = inq.idInquilino";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Contrato c = new Contrato
                        {
                            idContrato = reader.GetInt32(0),
                            idInquilino = reader.GetInt32(1),
                            idInmueble = reader.GetInt32(2),
                            FechaInicio = reader.GetDateTime(3),
                            FechaFin = reader.GetDateTime(4),
                            MontoAlquiler = reader.GetDecimal(5),
                            Vigente = reader.GetBoolean(6),

                            Inquilinos = new Inquilino
                            {
                                idInquilino = reader.GetInt32(1),
                                Nombre = reader.GetString(7),
                                Apellido = reader.GetString(8),
                            },

                            Inmuebles = new Inmueble
                            {
                                idInmueble = reader.GetInt32(2),
                                Direccion = reader.GetString(9)
                            }


                        }; res.Add(c);
                    }

                }
                connection.Close();
            }

            return res;

        }

        public IList<Contrato> ObtenerTodosPorInm(int id)
        {
            IList<Contrato> res = new List<Contrato>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idContrato, c.idInquilino, c.idInmueble, FechaInicio, FechaFin, MontoAlquiler, Vigente," +
                    $" inq.Nombre, inq.Apellido," +
                    $" inm.Direccion, inm.Tipo" +
                    $" FROM Contratos c INNER JOIN Inmuebles inm ON c.idInmueble = inm.idInmueble INNER JOIN Inquilinos inq ON c.idInquilino = inq.idInquilino" +
                    $" WHERE c.idInmueble = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Contrato c = new Contrato
                        {
                            idContrato = reader.GetInt32(0),
                            idInquilino = reader.GetInt32(1),
                            idInmueble = reader.GetInt32(2),
                            FechaInicio = reader.GetDateTime(3),
                            FechaFin = reader.GetDateTime(4),
                            MontoAlquiler = reader.GetDecimal(5),
                            Vigente = reader.GetBoolean(6),

                            Inquilinos = new Inquilino
                            {
                                Nombre = reader.GetString(7),
                                Apellido = reader.GetString(8),
                            },

                            Inmuebles = new Inmueble
                            {
                                Direccion = reader.GetString(9),
                                Tipo = reader.GetString(10)
                            }


                        }; res.Add(c);
                    }

                }
                connection.Close();
            }

            return res;

        }

        public Contrato ObtenerPorId(int id)
        {
            Contrato con = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $" SELECT idContrato, c.idInquilino, c.idInmueble, FechaInicio, FechaFin, MontoAlquiler, Vigente, " +
                    $" inq.Nombre, inq.Apellido ," +
                    $" inm.Direccion, inm.Tipo" +
                    $" FROM Contratos c INNER JOIN Inmuebles inm ON c.idInmueble = inm.idInmueble " +
                    $" INNER JOIN Inquilinos inq ON c.idInquilino = inq.idInquilino " +
                    $" WHERE c.idContrato = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        con = new Contrato
                        {
                            idContrato = reader.GetInt32(0),
                            idInquilino = reader.GetInt32(1),
                            idInmueble = reader.GetInt32(2),
                            FechaInicio = reader.GetDateTime(3),
                            FechaFin = reader.GetDateTime(4),
                            MontoAlquiler = reader.GetDecimal(5),
                            Vigente = reader.GetBoolean(6),


                            Inquilinos = new Inquilino
                            {
                                Nombre = reader.GetString(7),
                                Apellido = reader.GetString(8),
                            },

                            Inmuebles = new Inmueble
                            {
                                Direccion = reader.GetString(9),
                                Tipo = reader.GetString(10)
                            }


                        };
                    }

                }
                connection.Close();
            }

            return con;

        }
        public IList<Contrato> VigentesPorFecha(DateTime f)
        {
            List<Contrato> res = new List<Contrato>();
            Contrato con = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idContrato, c.idInquilino, c.idInmueble, FechaInicio, FechaFin, MontoAlquiler, Vigente, " +
                    $"i.Nombre, i.Apellido, " +
                    $"inm.Direccion, inm.Tipo" +
                    $" FROM Contratos c INNER JOIN Inquilinos i ON c.idInquilino = i.idInquilino INNER JOIN Inmuebles inm ON c.idInmueble = inm.idInmueble " +
                    $"WHERE  @fechaConsulta BETWEEN FechaInicio AND FechaFin AND Vigente = {1} ";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@fechaConsulta", SqlDbType.DateTime).Value = f;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        con = new Contrato
                        {
                            idContrato = reader.GetInt32(0),
                            idInmueble = reader.GetInt32(1),
                            idInquilino = reader.GetInt32(2),
                            FechaInicio = reader.GetDateTime(3),
                            FechaFin = reader.GetDateTime(4),
                            MontoAlquiler = reader.GetDecimal(5),
                            Vigente = reader.GetBoolean(6),
                            Inquilinos = new Inquilino
                            {
                                Nombre = reader.GetString(7),
                                Apellido = reader.GetString(8)
                            },
                            Inmuebles = new Inmueble
                            {
                                Direccion = reader.GetString(9),
                                Tipo = reader.GetString(10)
                            }


                        };
                        res.Add(con);
                    }
                    connection.Close();
                }
            }
            return res;
        }
        public Contrato ObtenerPorInm(int id)
        {
            Contrato con = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $" SELECT idContrato, c.idInquilino, c.idInmueble, FechaInicio, FechaFin, MontoAlquiler, Vigente, " +
                    $" inq.Nombre, inq.Apellido ," +
                    $" inm.Direccion, inm.Tipo" +
                    $" FROM Contratos c INNER JOIN Inmuebles inm ON c.idInmueble = inm.idInmueble " +
                    $" INNER JOIN Inquilinos inq ON c.idInquilino = inq.idInquilino " +
                    $" WHERE c.idInmueble = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        con = new Contrato
                        {
                            idContrato = reader.GetInt32(0),
                            idInquilino = reader.GetInt32(1),
                            idInmueble = reader.GetInt32(2),
                            FechaInicio = reader.GetDateTime(3),
                            FechaFin = reader.GetDateTime(4),
                            MontoAlquiler = reader.GetDecimal(5),
                            Vigente = reader.GetBoolean(6),


                            Inquilinos = new Inquilino
                            {
                                Nombre = reader.GetString(7),
                                Apellido = reader.GetString(8),
                            },

                            Inmuebles = new Inmueble
                            {
                                Direccion = reader.GetString(9),
                                Tipo = reader.GetString(10)
                            }


                        };
                    }

                }
                connection.Close();
            }

            return con;

        }
    }
}
