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
    public class EstadiaRepository
    {
        public int Insertar(Estadia e)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = @"INSERT INTO Estadia (IdHabitacion, IdHuesped, FechaEntrada, Temporada, Estado)
                                OUTPUT INSERTED.IdEstadia
                                VALUES (@idHab, @idHuesped, @fechaEntrada, @temporada, 'Activa')";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idHab", e.IdHabitacion);
                    cmd.Parameters.AddWithValue("@idHuesped", e.IdHuesped);
                    cmd.Parameters.AddWithValue("@fechaEntrada", e.FechaEntrada);
                    cmd.Parameters.AddWithValue("@temporada", e.Temporada);
                    cn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public Estadia ObtenerActivaPorHabitacion(int idHabitacion)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = @"SELECT IdEstadia, IdHabitacion, IdHuesped, FechaEntrada, FechaSalida, Temporada, Estado
                                FROM Estadia WHERE IdHabitacion = @idHab AND Estado = 'Activa'";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idHab", idHabitacion);
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Estadia
                            {
                                Id = (int)reader["IdEstadia"],
                                IdHabitacion = (int)reader["IdHabitacion"],
                                IdHuesped = (int)reader["IdHuesped"],
                                FechaEntrada = (DateTime)reader["FechaEntrada"],
                                Temporada = reader["Temporada"].ToString(),
                                Estado = reader["Estado"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void Cerrar(int idEstadia)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "UPDATE Estadia SET FechaSalida = @fecha, Estado = 'Cerrada' WHERE IdEstadia = @id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("@id", idEstadia);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
