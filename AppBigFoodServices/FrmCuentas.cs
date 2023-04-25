using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BLL;
using DAL;

namespace AppBigFoodServices
{
    public partial class FrmCuentas : Form
    {
        private Conexion conexion;
        private Cuentas cuentas;

        public FrmCuentas()
        {
            InitializeComponent();
            this.conexion = new Conexion(FrmPrincipal.ObtenerStringConexion());
        }//fin del metodo constructor

        private void txtCedulaCliente_TextChanged(object sender, EventArgs e)
        {
            // Try para manejar un posible error durante el proceso de busqueda
            try
            {
                this.BuscarDatosCuentas(this.txtCedulaCliente.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarDatosCuentas(string pCedula)
        {
            try // Se maneja un try para controlar un posible error durante el proceso de utilizacion
                // del metodo de busqueda.
            {
                this.dtgDatosCuentas.DataSource = this.conexion.BuscarCuentas(pCedula).Tables[0];
                this.dtgDatosCuentas.AutoResizeColumns();
                this.dtgDatosCuentas.ReadOnly = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }//fin del metodo de buscar clientes por nombre

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }//fin de la clase
}//fin de namespace
