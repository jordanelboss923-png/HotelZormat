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
    public class UsuarioRepository
    {
        public Usuario ValidarCredenciales(string nombreUsuario, string contrasena)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "SELECT IdUsuario, NombreUsuario, Contrasena, Rol, NombreCompleto " +
                             "FROM Usuario WHERE NombreUsuario = @usuario AND Contrasena = @clave";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@clave", contrasena);
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Id = (int)reader["IdUsuario"],
                                NombreUsuario = reader["NombreUsuario"].ToString(),
                                Rol = reader["Rol"].ToString(),
                                NombreCompleto = reader["NombreCompleto"].ToString()
                            };
                        }
                    }
                }
            }
            return null; // usuario/clave incorrectos
        }
    }
}
