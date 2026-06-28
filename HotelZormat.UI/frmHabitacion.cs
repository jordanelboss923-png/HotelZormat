using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelZormat.Negocio.Modelo;
using HotelZormat.Negocio.Servicios;

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

        }
        private void CargarHabitacionesPiso3()
        {
            lstHabitaciones.Items.Clear();

            foreach (Habitacion h in servicio.ObtenerHabitaciones())
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
    }
}
