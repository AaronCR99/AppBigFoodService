using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DAL;

namespace AppBigFoodServices
{
    public partial class FrmAgregarCliente : Form
    {
        private Conexion conexion;

        public FrmAgregarCliente()
        {
            InitializeComponent();
            this.conexion = new Conexion(FrmPrincipal.ObtenerStringConexion());
        }


        private void ValidacionesDatosRequeridos()
        {
            try
            {
                if (this.txtCedula.Text.Trim().Equals(""))
                {
                    throw new Exception("Se necisita digitar la cedula");
                }
                if (this.cbxTipoCedula.Text.Trim().Equals(""))
                {
                    throw new Exception("Agergue un tipo de cedula");
                }
                if (this.txtNombreCompleto.Text.Trim().Equals(""))
                {
                    throw new Exception("No se permite agreagar sin nombre completo");
                }
                if (this.txtEmail.Text.Trim().Equals(""))
                {
                    throw new Exception("No se permite el email en blanco");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarClient_Click(object sender, EventArgs e)
        {
            try
            {
                ValidacionesDatosRequeridos();
                this.conexion.guardarNuevoCliente(this.txtCedula.Text, cbxTipoCedula.SelectedItem.ToString(), txtNombreCompleto.Text, txtEmail.Text, "Admin");
                MessageBox.Show("El cliente se agrego correctamente");
                limpiarCampos();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void limpiarCampos()
        {
            txtCedula.Text = "";
            cbxTipoCedula.Text = "";
            txtNombreCompleto.Text = "";
            txtEmail.Text = "";
        }
    }
}
