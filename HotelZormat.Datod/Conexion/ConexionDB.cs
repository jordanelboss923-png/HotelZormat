using System;
using System.Configuration;
using System.Data.SqlClient;

namespace HotelZormat.Datos.Conexion
{
    public static class ConexionBD
    {
        public static SqlConnection ObtenerConexion()
        {
            string cadena = ConfigurationManager
                .ConnectionStrings["HotelZormatDB"].ConnectionString;
            return new SqlConnection(cadena);
        }
    }
}