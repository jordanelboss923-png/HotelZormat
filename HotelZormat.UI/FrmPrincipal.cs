using HotelZormat.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelZormat.UI
{
    public partial class FrmPrincipal : Form
    {
        private Usuario usuarioActual;
        public FrmPrincipal(Usuario usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            this.Text = "HotelZormat - " + usuarioActual.NombreCompleto + " (" + usuarioActual.Rol + ")";

            // Ejemplo: solo el Administrador puede eliminar habitaciones/huéspedes
            if (!usuarioActual.EsAdministrador())
            {
                // btnEliminarHabitacion.Enabled = false; // ajusta al nombre real de tu botón/menú
            }
        }

        private void btnHuesped_Click(object sender, EventArgs e)
        {
            frmHuesped f = new frmHuesped();
            f.ShowDialog();
        }

        private void btnHabitacion_Click(object sender, EventArgs e)
        {
            frmHabitacion f = new frmHabitacion();
            f.ShowDialog();
        }
    }
}
