using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelZormat.Modelo;
using HotelZormat.Datos.Conexion;

namespace HotelZormat.Datos.Repositorios
{
    public class HuespedRepository
    {
        public List<Huesped> ObtenerTodos()
        {
            var lista = new List<Huesped>();
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "SELECT IdHuesped, Nombre, Apellido, TipoDocumento, NumeroDocumento, " +
                             "Nacionalidad, Telefono, Email FROM Huesped";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MapearHuesped(reader));
                        }
                    }
                }
            }
            return lista;
        }

        public Huesped BuscarPorDocumento(string numeroDocumento)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "SELECT IdHuesped, Nombre, Apellido, TipoDocumento, NumeroDocumento, " +
                             "Nacionalidad, Telefono, Email FROM Huesped WHERE NumeroDocumento = @doc";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@doc", numeroDocumento);
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapearHuesped(reader);
                    }
                }
            }
            return null;
        }

        public void Insertar(Huesped h)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = @"INSERT INTO Huesped
                    (Nombre, Apellido, TipoDocumento, NumeroDocumento, Nacionalidad, Telefono, Email)
                    VALUES
                    (@nombre, @apellido, @tipoDoc, @numDoc, @nacionalidad, @telefono, @email)";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    AgregarParametros(cmd, h);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Huesped h)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = @"UPDATE Huesped SET
                    Nombre=@nombre, Apellido=@apellido, TipoDocumento=@tipoDoc,
                    Nacionalidad=@nacionalidad, Telefono=@telefono, Email=@email
                    WHERE NumeroDocumento=@numDoc";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    AgregarParametros(cmd, h);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(string numeroDocumento)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "DELETE FROM Huesped WHERE NumeroDocumento = @numDoc";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@numDoc", numeroDocumento);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // --- Métodos privados de apoyo (evitan repetir código) ---

        private Huesped MapearHuesped(SqlDataReader reader)
        {
            return new Huesped
            {
                Id = (int)reader["IdHuesped"],
                Nombre = reader["Nombre"].ToString(),
                Apellido = reader["Apellido"].ToString(),
                TipoDocumento = reader["TipoDocumento"].ToString(),
                NumeroDocumento = reader["NumeroDocumento"].ToString(),
                Nacionalidad = reader["Nacionalidad"].ToString(),
                Telefono = reader["Telefono"].ToString(),
                Email = reader["Email"].ToString()
            };
        }

        private void AgregarParametros(SqlCommand cmd, Huesped h)
        {
            cmd.Parameters.AddWithValue("@nombre", h.Nombre);
            cmd.Parameters.AddWithValue("@apellido", h.Apellido);
            cmd.Parameters.AddWithValue("@tipoDoc", h.TipoDocumento);
            cmd.Parameters.AddWithValue("@numDoc", h.NumeroDocumento);
            cmd.Parameters.AddWithValue("@nacionalidad", h.Nacionalidad);
            cmd.Parameters.AddWithValue("@telefono", h.Telefono);
            cmd.Parameters.AddWithValue("@email", h.Email);
        }
    }
}