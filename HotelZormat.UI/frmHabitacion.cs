using HotelZormat.Modelo;
using HotelZormat.Negocio.Servicios;
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
    public partial class frmHabitacion : Form
    {
        private HabitacionService servicio = new HabitacionService();
        public frmHabitacion()
        {
            InitializeComponent();
        }

        private void frmHabitacion_Load(object sender, EventArgs e)
        {
            cboTipo.Items.Clear();

            cboTipo.Items.Add("Simple");
            cboTipo.Items.Add("Doble");
            cboTipo.Items.Add("Suite");

            cboAccion.Items.Clear();

            cboAccion.Items.Add("Check In");
            cboAccion.Items.Add("Check Out");
            cboAccion.Items.Add("Reservar");
            cboAccion.Items.Add("Limpiar");

            CargarHabitacionesPiso3();

            cboTemporada.Items.Clear();
            cboTemporada.Items.Add("Alta");
            cboTemporada.Items.Add("Media");
            cboTemporada.Items.Add("Baja");

        }
        private void CargarHabitacionesPiso3()
        {
            lstHabitaciones.Items.Clear();

            foreach (Habitacion h in servicio.ObtenerTodas())
            {
                if (h.Piso == 3)
                {
                    lstHabitaciones.Items.Add(
                        h.Numero + " - " +
                        h.Tipo + " - " +
                        h.Estado);
                }
            }
        }
        private void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboTipo.Text)
            {
                case "Simple":
                    lblIcono.Text = "🛏️";
                    break;

                case "Doble":
                    lblIcono.Text = "🛌";
                    break;

                case "Suite":
                    lblIcono.Text = "🏦";
                    break;

                default:
                    lblIcono.Text = "❓";
                    break;
            }
            try
            {
                label6.Text = "Tarifa: RD$ " + ObtenerTarifa(cboTipo.Text).ToString("N2");
            }
            catch (ArgumentException ex)
            {
                label6.Text = ex.Message;
            }
        }
        private decimal ObtenerTarifa(string tipo)
        {
            switch (tipo)
            {
                case "Simple":
                    return 2500;

                case "Doble":
                    return 4000;

                case "Suite":
                    return 7000;

                default:
                    throw new ArgumentException("Tipo inválido");
            }
        }
        private void CambiarColorEstado(string estado)
        {
            switch (estado)
            {
                case "Disponible":
                    lblEstado.ForeColor = Color.Green;
                    break;

                case "Ocupada":
                    lblEstado.ForeColor = Color.Red;
                    break;

                case "Reservada":
                    lblEstado.ForeColor = Color.Blue;
                    break;

                case "Limpieza":
                    lblEstado.ForeColor = Color.Orange;
                    break;

                default:
                    lblEstado.ForeColor = Color.Black;
                    break;
            }

            lblEstado.Text = estado;
        }
        private void ConfigurarBotones(string estado)
        {
            btnCheckIn.Enabled = false;
            btnCheckOut.Enabled = false;
            btnReservar.Enabled = false;
            btnLimpiar.Enabled = false;

            switch (estado)
            {
                case "Disponible":
                    btnCheckIn.Enabled = true;
                    break;

                case "Ocupada":
                    btnCheckOut.Enabled = true;
                    break;

                case "Reservada":
                    btnReservar.Enabled = true;
                    break;

                case "Limpieza":
                    btnLimpiar.Enabled = true;
                    break;
            }
        }

        private EstadiaService servicioEstadia = new EstadiaService();
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int numeroHab = Convert.ToInt32(txtNumero.Text);
                Habitacion habitacion = servicio.Buscar(numeroHab);
                if (habitacion == null) { MessageBox.Show("Habitación no encontrada."); return; }

                switch (cboAccion.Text)
                {
                    case "Check In":
                        HuespedService servicioHuesped = new HuespedService();
                        Huesped huesped = servicioHuesped.Buscar(txtDocumentoHuesped.Text);
                        if (huesped == null) { MessageBox.Show("Huésped no encontrado."); return; }

                        DialogResult r1 = MessageBox.Show("¿Confirmar Check In?", "Confirmar",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r1 == DialogResult.Yes)
                        {
                            servicioEstadia.RealizarCheckIn(habitacion.Id, huesped.Id, cboTemporada.Text);
                            MessageBox.Show("Check In realizado.");
                            CargarHabitacionesPiso3();
                        }
                        break;

                    case "Check Out":
                        DialogResult r2 = MessageBox.Show("¿Confirmar Check Out?", "Confirmar",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r2 == DialogResult.Yes)
                        {
                            int noches = (int)numNoches.Value;
                            Factura f = servicioEstadia.RealizarCheckOut(habitacion.Id, noches);
                            MessageBox.Show(
                                "Factura generada.\nNCF: " + f.NCF +
                                "\nSubtotal: RD$" + f.Subtotal.ToString("N2") +
                                "\nITBIS: RD$" + f.ITBIS.ToString("N2") +
                                "\nPropina: RD$" + f.Propina.ToString("N2") +
                                "\nTotal: RD$" + f.Total.ToString("N2"),
                                "Factura");
                            CargarHabitacionesPiso3();
                        }
                        break;

                    default:
                        MessageBox.Show("Seleccione Check In o Check Out.");
                        break;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Datos inválidos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HabitacionOcupadaException ex)
            {
                MessageBox.Show("La habitación " + ex.NumeroHabitacion + " está ocupada.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int numero = Convert.ToInt32(txtNumero.Text);

                Habitacion habitacion = servicio.Buscar(numero);

                if (habitacion != null)
                {
                    cboTipo.Text = habitacion.Tipo;
                    CambiarColorEstado(habitacion.Estado);
                    ConfigurarBotones(habitacion.Estado);
                }
                else
                {
                    MessageBox.Show("La habitación no existe.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(
                    "Debe escribir un número válido.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;

            try
            {
                Habitacion h = new Habitacion();

                h.Numero = Convert.ToInt32(txtNumero.Text);
                h.Piso = 3;
                h.Tipo = cboTipo.Text;
                h.Capacidad = 2;
                h.TarifaBase = ObtenerTarifa(cboTipo.Text);

                h.Estado = (lblEstado.Text == "Estado:" || string.IsNullOrWhiteSpace(lblEstado.Text))
                    ? "Disponible"
                    : lblEstado.Text;

                servicio.Guardar(h);

                MessageBox.Show("Habitación guardada correctamente.");

                CargarHabitacionesPiso3();
            }
            catch (FormatException)
            {
                MessageBox.Show("El número de habitación debe ser numérico.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HabitacionOcupadaException ex)
            {
                MessageBox.Show("La habitación " + ex.NumeroHabitacion + " está ocupada.");
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
            finally
            {
                btnGuardar.Enabled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int numero = Convert.ToInt32(txtNumero.Text);

                DialogResult respuesta = MessageBox.Show(
                    "¿Seguro que desea eliminar la habitación " + numero + "?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    servicio.Eliminar(numero);
                    MessageBox.Show("Habitación eliminada.");
                    CargarHabitacionesPiso3();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Debe escribir un número válido.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HabitacionOcupadaException ex)
            {
                MessageBox.Show("No se puede eliminar: la habitación " + ex.NumeroHabitacion + " está ocupada.");
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
