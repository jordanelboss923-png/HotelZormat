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
    public class BitacoraRepository
    {
        public void Registrar(int idUsuario, string accion)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "INSERT INTO Bitacora (IdUsuario, Accion) VALUES (@idUsuario, @accion)";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@accion", accion);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<RegistroBitacora> ObtenerTodos()
        {
            var lista = new List<RegistroBitacora>();
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = @"SELECT b.IdBitacora, b.IdUsuario, u.NombreUsuario, b.Accion, b.FechaHora
                                FROM Bitacora b
                                INNER JOIN Usuario u ON b.IdUsuario = u.IdUsuario
                                ORDER BY b.FechaHora DESC";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new RegistroBitacora
                            {
                                Id = (int)reader["IdBitacora"],
                                IdUsuario = (int)reader["IdUsuario"],
                                NombreUsuario = reader["NombreUsuario"].ToString(),
                                Accion = reader["Accion"].ToString(),
                                FechaHora = (System.DateTime)reader["FechaHora"]
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
