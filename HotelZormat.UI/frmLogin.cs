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
    public partial class frmLogin : Form
    {
        private UsuarioService servicio = new UsuarioService();
        public Usuario UsuarioLogueado { get; private set; }
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario u = servicio.IniciarSesion(txtUsuario.Text, txtContrasena.Text);

                if (u != null)
                {
                    UsuarioLogueado = u;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblMensaje.Text = "Usuario o contraseña incorrectos.";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}
