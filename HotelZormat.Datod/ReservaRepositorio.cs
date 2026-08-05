using HotelZormat.Datos.Conexion;
using HotelZormat.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Datod
{
    public class ReservaRepository
    {
        public void Insertar(Reserva r)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = @"INSERT INTO Reserva (IdHabitacion, IdHuesped, FechaEntradaEstimada, Estado)
                                VALUES (@idHab, @idHuesped, @fecha, 'Pendiente')";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idHab", r.IdHabitacion);
                    cmd.Parameters.AddWithValue("@idHuesped", r.IdHuesped);
                    cmd.Parameters.AddWithValue("@fecha", r.FechaEntradaEstimada);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Reserva> ObtenerTodas()
        {
            var lista = new List<Reserva>();
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = @"SELECT r.IdReserva, r.IdHabitacion, r.IdHuesped, r.FechaReserva,
                                       r.FechaEntradaEstimada, r.Estado,
                                       h.Numero AS NumeroHabitacion,
                                       (hu.Nombre + ' ' + hu.Apellido) AS NombreHuesped
                                FROM Reserva r
                                INNER JOIN Habitacion h ON r.IdHabitacion = h.IdHabitacion
                                INNER JOIN Huesped hu ON r.IdHuesped = hu.IdHuesped
                                ORDER BY r.FechaEntradaEstimada";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Reserva
                            {
                                Id = (int)reader["IdReserva"],
                                IdHabitacion = (int)reader["IdHabitacion"],
                                IdHuesped = (int)reader["IdHuesped"],
                                NumeroHabitacion = reader["NumeroHabitacion"].ToString(),
                                NombreHuesped = reader["NombreHuesped"].ToString(),
                                FechaReserva = (DateTime)reader["FechaReserva"],
                                FechaEntradaEstimada = (DateTime)reader["FechaEntradaEstimada"],
                                Estado = reader["Estado"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public void CambiarEstado(int idReserva, string nuevoEstado)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "UPDATE Reserva SET Estado = @estado WHERE IdReserva = @id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@id", idReserva);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
