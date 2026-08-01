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
    public class FacturaRepository
    {
        public void Insertar(Factura f)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = @"INSERT INTO Factura (IdEstadia, NCF, Subtotal, ITBIS, Propina, Total)
                                VALUES (@idEstadia, @ncf, @subtotal, @itbis, @propina, @total)";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idEstadia", f.IdEstadia);
                    cmd.Parameters.AddWithValue("@ncf", f.NCF);
                    cmd.Parameters.AddWithValue("@subtotal", f.Subtotal);
                    cmd.Parameters.AddWithValue("@itbis", f.ITBIS);
                    cmd.Parameters.AddWithValue("@propina", f.Propina);
                    cmd.Parameters.AddWithValue("@total", f.Total);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Numeración secuencial simple: cuenta cuántas facturas hay + 1
        public int ContarFacturas()
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                string sql = "SELECT COUNT(*) FROM Factura";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
