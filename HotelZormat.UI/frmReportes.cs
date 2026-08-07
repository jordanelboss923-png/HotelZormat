// Cédula: 402-1145957-9
using HotelZormat.Negociod.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelZormat.UI
{
    public partial class frmReportes : Form
    {
        private ReporteServicio servicio = new ReporteServicio();
        public frmReportes()
        {
            InitializeComponent();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            ConfigurarGrids();
            CargarOcupacion();

            dtpDesde.Value = DateTime.Now.AddDays(-30);
            dtpHasta.Value = DateTime.Now;
        }

        private void ConfigurarGrids()
        {
            dgvOcupacion.AllowUserToAddRows = false;
            dgvOcupacion.ReadOnly = true;
            dgvOcupacion.Columns.Clear();
            dgvOcupacion.Columns.Add("Estado", "Estado");
            dgvOcupacion.Columns.Add("Cantidad", "Cantidad de habitaciones");

            dgvIngresos.AllowUserToAddRows = false;
            dgvIngresos.ReadOnly = true;
            dgvIngresos.Columns.Clear();
            dgvIngresos.Columns.Add("NCF", "NCF");
            dgvIngresos.Columns.Add("Subtotal", "Subtotal");
            dgvIngresos.Columns.Add("ITBIS", "ITBIS");
            dgvIngresos.Columns.Add("Propina", "Propina");
            dgvIngresos.Columns.Add("Total", "Total");
            dgvIngresos.Columns.Add("Fecha", "Fecha");
        }

        private void CargarOcupacion()
        {
            try
            {
                dgvOcupacion.Rows.Clear();
                Dictionary<string, int> ocupacion = servicio.ObtenerOcupacion();

                foreach (var item in ocupacion)
                {
                    dgvOcupacion.Rows.Add(item.Key, item.Value);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error de conexión con la base de datos: " + ex.Message,
                    "Error de base de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                dgvIngresos.Rows.Clear();

                List<object[]> facturas = servicio.ObtenerIngresos(dtpDesde.Value, dtpHasta.Value);

                foreach (object[] f in facturas)
                {
                    dgvIngresos.Rows.Add(
                        f[0],
                        ((decimal)f[1]).ToString("N2"),
                        ((decimal)f[2]).ToString("N2"),
                        ((decimal)f[3]).ToString("N2"),
                        ((decimal)f[4]).ToString("N2"),
                        ((DateTime)f[5]).ToString("dd/MM/yyyy")
                    );
                }

                decimal total = servicio.CalcularTotalIngresos(facturas);
                lblTotalIngresos.Text = "Total del período: RD$ " + total.ToString("N2");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error de conexión con la base de datos: " + ex.Message,
                    "Error de base de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
    

