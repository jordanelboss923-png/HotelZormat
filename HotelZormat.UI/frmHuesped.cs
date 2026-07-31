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
using HotelZormat.Negocio.Servicios;

namespace HotelZormat.UI
{
    public partial class frmHuesped : Form
    {
        private HuespedService servicio = new HuespedService();
        public frmHuesped()
        {
            InitializeComponent();
        }

        private void frmHuesped_Load(object sender, EventArgs e)
        {
            cboTipoDocumento.Items.Clear();
            cboTipoDocumento.Items.Add("Cedula");
            cboTipoDocumento.Items.Add("Pasaporte");

            CargarLista();
        }

        private void CargarLista()
        {
            lstHuespedes.Items.Clear();
            foreach (Huesped h in servicio.ObtenerTodos())
            {
                lstHuespedes.Items.Add(h.NumeroDocumento + " - " + h.NombreCompleto());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("Escriba un número de documento.");
                return;
            }

            Huesped h = servicio.Buscar(txtDocumento.Text);

            if (h != null)
            {
                cboTipoDocumento.Text = h.TipoDocumento;
                txtNombre.Text = h.Nombre;
                txtApellido.Text = h.Apellido;
                txtNacionalidad.Text = h.Nacionalidad;
                txtTelefono.Text = h.Telefono;
                txtEmail.Text = h.Email;
            }
            else
            {
                MessageBox.Show("No se encontró ningún huésped con ese documento.");
                LimpiarCampos();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;

            try
            {
                Huesped h = new Huesped
                {
                    NumeroDocumento = txtDocumento.Text,
                    TipoDocumento = cboTipoDocumento.Text,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Nacionalidad = txtNacionalidad.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text
                };

                servicio.Guardar(h);

                MessageBox.Show("Huésped guardado correctamente.");
                CargarLista();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("Escriba el documento del huésped a eliminar.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que desea eliminar este huésped?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    servicio.Eliminar(txtDocumento.Text);
                    MessageBox.Show("Huésped eliminado.");
                    CargarLista();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtDocumento.Text = "";
            cboTipoDocumento.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtNacionalidad.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
        }
    }
}
