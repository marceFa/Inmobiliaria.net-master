using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inmobiliaria.Models
{
    public class RepositorioPago
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;

        public RepositorioPago(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public int Alta(Pago p)
        {
            int res = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Pagos(Numero, idContrato, FechaDePago, Importe)" +
                    $"VALUES (@pago, @idContrato, @fec, @importe);" +
                    $"SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@pago", p.Numero);
                    command.Parameters.AddWithValue("@idContrato", p.idContrato);
                    command.Parameters.AddWithValue("@fec", p.FechaDePago);
                    command.Parameters.AddWithValue("@importe", p.Importe);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    p.idPago = res;
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
                string sql = $"DELETE FROM Pagos WHERE idPago=@id";

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

        public int Modificacion(Pago p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Pagos SET  Importe=@importe " +
                            $"WHERE IdPago=@id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@importe", p.Importe);
                    command.Parameters.AddWithValue("@id", p.idPago);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return res;
        }

        public IList<Pago> ObtenerTodos()
        {
            IList<Pago> res = new List<Pago>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idPago, Numero, p.idContrato, FechaDePago, Importe, " +
                    $"c.idInmueble, c.idInquilino," +
                    $"Inm.Direccion, Inm.Tipo," +
                    $"  Inq.Nombre, Inq.Apellido FROM Pagos p INNER JOIN Contratos c ON p.idContrato = c.idContrato INNER JOIN Inmuebles Inm ON Inm.idInmueble = c.idInmueble INNER JOIN Inquilinos Inq ON Inq.idInquilino = c.idInquilino";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Pago p = new Pago
                        {
                            idPago = reader.GetInt32(0),
                            Numero = reader.GetInt32(1),
                            idContrato = reader.GetInt32(2),
                            FechaDePago = reader.GetDateTime(3),
                            Importe = reader.GetDecimal(4),
                            Contratos = new Contrato
                            {
                                idInmueble = reader.GetInt32(5),
                                idInquilino = reader.GetInt32(6),
                                Inmuebles = new Inmueble
                                {
                                    Direccion = reader.GetString(7),
                                    Tipo = reader.GetString(8),
                                },
                                Inquilinos = new Inquilino
                                {
                                    Nombre = reader.GetString(9),
                                    Apellido = reader.GetString(10),
                                }
                            }

                        };
                        res.Add(p);
                    }
                    connection.Close();
                }
            }
            return res;
        }
        public Pago ObtenerPorId(int id)
        {
            Pago p = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idPago, Numero, p.idContrato,FechaDePago, Importe, " +
                     $"c.idInquilino, c.idInquilino," +
                    $"Inm.Direccion, Inm.Tipo," +
                    $"Inq.Nombre, Inq.Apellido FROM Pagos p INNER JOIN Contratos c ON c.idContrato=p.idContrato " +
                    $"INNER JOIN Inmuebles Inm ON Inm.idInmueble = c.idInmueble " +
                    $"INNER JOIN Inquilinos Inq ON Inq.idInquilino = c.idInquilino " +
                    $"WHERE p.IdPago=@id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        p = new Pago
                        {
                            idPago = reader.GetInt32(0),
                            Numero = reader.GetInt32(1),
                            idContrato = reader.GetInt32(2),
                            FechaDePago = reader.GetDateTime(3),
                            Importe = reader.GetDecimal(4),
                            Contratos = new Contrato
                            {
                                idInmueble = reader.GetInt32(5),
                                idInquilino = reader.GetInt32(6),
                                Inmuebles = new Inmueble
                                {
                                    Direccion = reader.GetString(7),
                                    Tipo = reader.GetString(8),
                                },
                                Inquilinos = new Inquilino
                                {
                                    Nombre = reader.GetString(9),
                                    Apellido = reader.GetString(10)
                                }
                            }
                        };
                    }
                    connection.Close();
                }
            }
            return p;
        }


        public IList<Pago> ObtenerPorContr(int id)
        {
            IList<Pago> res = new List<Pago>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idPago, Numero, p.idContrato,FechaDePago, Importe, " +
                     $"c.idInquilino, c.idInquilino," +
                    $"Inm.Direccion, Inm.Tipo," +
                    $"Inq.Nombre, Inq.Apellido FROM Pagos p INNER JOIN Contratos c ON c.idContrato=p.idContrato " +
                    $"INNER JOIN Inmuebles Inm ON Inm.idInmueble = c.idInmueble " +
                    $"INNER JOIN Inquilinos Inq ON Inq.idInquilino = c.idInquilino " +
                    $"WHERE p.idContrato=@id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Pago p = new Pago
                        {
                            idPago = reader.GetInt32(0),
                            Numero = reader.GetInt32(1),
                            idContrato = reader.GetInt32(2),
                            FechaDePago = reader.GetDateTime(3),
                            Importe = reader.GetDecimal(4),
                            Contratos = new Contrato
                            {
                                idInmueble = reader.GetInt32(5),
                                idInquilino = reader.GetInt32(6),
                                Inmuebles = new Inmueble
                                {
                                    Direccion = reader.GetString(7),
                                    Tipo = reader.GetString(8),
                                },
                                Inquilinos = new Inquilino
                                {
                                    Nombre = reader.GetString(9),
                                    Apellido = reader.GetString(10)
                                }
                            }
                        };
                        res.Add(p);
                    }
                    connection.Close();
                }
            }
            return res;

        }
    }
}



