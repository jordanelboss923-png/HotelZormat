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
    public partial class frmReservas : Form
    {
        private ReservaServicio servicio = new ReservaServicio();
        public frmReservas()
        {
            InitializeComponent();
        }

        private void frmReservas_Load(object sender, EventArgs e)
        {
            ConfigurarColumnas();
            CargarLista();
        }

        private void ConfigurarColumnas()
        {
            dgvReservas.Columns.Clear();
            dgvReservas.Columns.Add("IdReserva", "Id");
            dgvReservas.Columns.Add("Habitacion", "Habitación");
            dgvReservas.Columns.Add("Huesped", "Huésped");
            dgvReservas.Columns.Add("FechaEntrada", "Fecha de entrada");
            dgvReservas.Columns.Add("Estado", "Estado");

            // Ocultamos la columna del Id, solo la usamos internamente para saber cuál seleccionaron
            dgvReservas.Columns["IdReserva"].Visible = false;
        }

        private void CargarLista()
        {
            try
            {
                dgvReservas.Rows.Clear();

                List<Reserva> reservas = servicio.ObtenerTodas();

                foreach (Reserva r in reservas)
                {
                    dgvReservas.Rows.Add(
                        r.Id,
                        r.NumeroHabitacion,
                        r.NombreHuesped,
                        r.FechaEntradaEstimada.ToShortDateString(),
                        r.Estado
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

        private int? ObtenerIdSeleccionado()
        {
            if (dgvReservas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una reserva de la tabla.");
                return null;
            }

            DataGridViewRow fila = dgvReservas.SelectedRows[0];
            return Convert.ToInt32(fila.Cells["IdReserva"].Value);
        }
        private void btnConfirmarReserva_Click(object sender, EventArgs e)
        {
            int? id = ObtenerIdSeleccionado();
            if (id == null) return;

            try
            {
                servicio.Confirmar(id.Value);
                MessageBox.Show("Reserva confirmada.");
                CargarLista();
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

        private void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            int? id = ObtenerIdSeleccionado();
            if (id == null) return;

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que desea cancelar esta reserva?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    servicio.Cancelar(id.Value);
                    MessageBox.Show("Reserva cancelada.");
                    CargarLista();
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarLista();
        }
    }
}
