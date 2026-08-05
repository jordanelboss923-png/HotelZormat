// Cédula: 402-1145957-9
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
        private int matricula = 20243553; // Jordan Alexander Guzman Cedano - 2024-3553
        public FrmPrincipal(Usuario usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;

            menuPrincipal.BackColor = Color.FromArgb(30, 30, 45);
            menuPrincipal.ForeColor = Color.White;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            this.Text = "HotelZormat - " + usuarioActual.NombreCompleto + " (" + usuarioActual.Rol + ")";

            if (!usuarioActual.EsAdministrador())
            {
                menuBitacora.Visible = false; // solo Admin ve Bitácora
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuHabitaciones_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmHabitacion f = new frmHabitacion();
            f.ShowDialog();
            this.Show();
        }

        private void menuHuespedes_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmHuesped f = new frmHuesped();
            f.ShowDialog();
            this.Show();
        }

        private void menuCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
        "¿Seguro que desea cerrar sesión?",
        "Confirmar",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                this.Hide();
                frmLogin login = new frmLogin();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    // Vuelve a cargar el usuario logueado y refresca el menú
                    FrmPrincipal nuevo = new FrmPrincipal(login.UsuarioLogueado);
                    nuevo.Show();
                }
                this.Close();
            }
        }
    }
}
