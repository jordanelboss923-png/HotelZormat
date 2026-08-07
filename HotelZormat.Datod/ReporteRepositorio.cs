using HotelZormat.Datos.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Datod
{
    public class ReporteRepository
    {
        // Reporte 1: cuántas habitaciones hay en cada estado
        public Dictionary<string, int> ObtenerOcupacionPorEstado()
        {
            var resultado = new Dictionary<string, int>();
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "SELECT Estado, COUNT(*) AS Cantidad FROM Habitacion GROUP BY Estado";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultado.Add(reader["Estado"].ToString(), (int)reader["Cantidad"]);
                        }
                    }
                }
            }
            return resultado;
        }

        // Reporte 2: facturas emitidas entre dos fechas, con su total
        public List<object[]> ObtenerIngresosPorRango(DateTime desde, DateTime hasta)
        {
            var lista = new List<object[]>();
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = @"SELECT NCF, Subtotal, ITBIS, Propina, Total, FechaEmision
                                FROM Factura
                                WHERE FechaEmision BETWEEN @desde AND @hasta
                                ORDER BY FechaEmision";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@desde", desde.Date);
                    cmd.Parameters.AddWithValue("@hasta", hasta.Date.AddDays(1).AddSeconds(-1));
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new object[]
                            {
                                reader["NCF"].ToString(),
                                (decimal)reader["Subtotal"],
                                (decimal)reader["ITBIS"],
                                (decimal)reader["Propina"],
                                (decimal)reader["Total"],
                                (DateTime)reader["FechaEmision"]
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
