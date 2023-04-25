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
    public partial class FrmListaClientes : Form
    {
        private Conexion conexion;
        public FrmListaClientes()
        {
            InitializeComponent();
            this.conexion = new Conexion(FrmPrincipal.ObtenerStringConexion());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            // Try para manejar un posible error durante el proceso de busqueda
            try
            {
                // se llama al metedo para realizr la busqueda
                this.BuscarClientePorNombre(this.txtNombreCliente.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarClientePorNombre(string pNombre)
        {
            try // Se maneja un try para controlar un posible error durante el proceso de utilizacion
                // del metodo de busqueda.
            {
                this.dtgDatosClientes.DataSource = this.conexion.BuscarClientes(pNombre).Tables[0];
                this.dtgDatosClientes.AutoResizeColumns();
                this.dtgDatosClientes.ReadOnly = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }//fin del metodo de buscar clientes por nombre
    }
}
