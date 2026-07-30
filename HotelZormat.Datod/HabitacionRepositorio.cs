using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelZormat.Modelo;
using HotelZormat.Datos.Conexion;

namespace HotelZormat.Datos.Repositorios
{
    public class HabitacionRepository
    {
        public List<Habitacion> ObtenerTodas()
        {
            var lista = new List<Habitacion>();
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "SELECT IdHabitacion, Numero, Tipo, Piso, Capacidad, TarifaBase, Estado FROM Habitacion";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Habitacion
                            {
                                Id = (int)reader["IdHabitacion"],
                                Numero = (int)reader["Numero"],
                                Tipo = reader["Tipo"].ToString(),
                                Piso = (int)reader["Piso"],
                                Capacidad = (int)reader["Capacidad"],
                                TarifaBase = (decimal)reader["TarifaBase"],
                                Estado = reader["Estado"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public Habitacion BuscarPorNumero(int numero)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "SELECT IdHabitacion, Numero, Tipo, Piso, Capacidad, TarifaBase, Estado " +
                             "FROM Habitacion WHERE Numero = @numero";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Habitacion
                            {
                                Id = (int)reader["IdHabitacion"],
                                Numero = (int)reader["Numero"],
                                Tipo = reader["Tipo"].ToString(),
                                Piso = (int)reader["Piso"],
                                Capacidad = (int)reader["Capacidad"],
                                TarifaBase = (decimal)reader["TarifaBase"],
                                Estado = reader["Estado"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void Actualizar(Habitacion h)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "UPDATE Habitacion SET Tipo=@tipo, Estado=@estado, " +
                             "Capacidad=@capacidad, TarifaBase=@tarifa WHERE Numero=@numero";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@tipo", h.Tipo);
                    cmd.Parameters.AddWithValue("@estado", h.Estado);
                    cmd.Parameters.AddWithValue("@capacidad", h.Capacidad);
                    cmd.Parameters.AddWithValue("@tarifa", h.TarifaBase);
                    cmd.Parameters.AddWithValue("@numero", h.Numero);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Insertar(Habitacion h)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = @"INSERT INTO Habitacion
                       (Numero, Tipo, Piso, Capacidad, TarifaBase, Estado)
                       VALUES
                       (@numero, @tipo, @piso, @capacidad, @tarifa, @estado)";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@numero", h.Numero);
                    cmd.Parameters.AddWithValue("@tipo", h.Tipo);
                    cmd.Parameters.AddWithValue("@piso", h.Piso);
                    cmd.Parameters.AddWithValue("@capacidad", h.Capacidad);
                    cmd.Parameters.AddWithValue("@tarifa", h.TarifaBase);
                    cmd.Parameters.AddWithValue("@estado", h.Estado);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int numero)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "DELETE FROM Habitacion WHERE Numero = @numero";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@numero", numero);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        
    }
}