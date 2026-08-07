// Cédula: 402-1145957-9
using HotelZormat.Modelo;
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
    public partial class frmBitacora : Form
    {
        private BitacoraServicio servicio = new BitacoraServicio();
        public frmBitacora()
        {
            InitializeComponent();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void frmBitacora_Load(object sender, EventArgs e)
        {
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.ReadOnly = true;
            dgvBitacora.MultiSelect = false;
            dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            ConfigurarColumnas();
            CargarLista();
        }

        private void ConfigurarColumnas()
        {
            dgvBitacora.Columns.Clear();
            dgvBitacora.Columns.Add("IdBitacora", "Id");
            dgvBitacora.Columns.Add("Usuario", "Usuario");
            dgvBitacora.Columns.Add("Accion", "Acción");
            dgvBitacora.Columns.Add("FechaHora", "Fecha y hora");

            dgvBitacora.Columns["IdBitacora"].Visible = false;
        }

        private void CargarLista()
        {
            try
            {
                dgvBitacora.Rows.Clear();

                List<RegistroBitacora> registros = servicio.ObtenerTodos();

                foreach (RegistroBitacora r in registros)
                {
                    dgvBitacora.Rows.Add(
                        r.Id,
                        r.NombreUsuario,
                        r.Accion,
                        r.FechaHora.ToString("dd/MM/yyyy hh:mm:ss tt")
                    );
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
    }
}
