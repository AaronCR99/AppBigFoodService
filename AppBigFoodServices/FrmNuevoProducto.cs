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
    public partial class FrmNuevoProducto : Form
    {
        private Conexion conexion;
        public FrmNuevoProducto()
        {
            InitializeComponent();
            this.conexion = new Conexion(FrmPrincipal.ObtenerStringConexion());
        }
        private void ValidacionesDatosRequeridos()
        {
            try
            {
                if (this.txtCodigo.Text.Trim().Equals(""))
                {
                    throw new Exception("Se necisita digitar un codigo");
                }
                if (this.cbxDescuentos.Text.Trim().Equals(""))
                {
                    throw new Exception("Agregue un descuento");
                }
                if (this.txtDescripcion.Text.Trim().Equals(""))
                {
                    throw new Exception("Se necesita ingresar una Descripcion");
                }
                if (this.cbxUnidadMedida.Text.Trim().Equals(""))
                {
                    throw new Exception("Se necesita unidad de medida");
                }
                if (this.cbImpuesto.Text.Trim().Equals(""))
                {
                    throw new Exception("Se necesita un impuesto");
                }
                if (this.txtPrecioCompra.Text.Trim().Equals(""))
                {
                    throw new Exception("Se necesita un precio compra");
                }
                if (this.txtPrecioVenta.Text.Trim().Equals(""))
                {
                    throw new Exception("Se necesita un precio venta");
                }
                if (this.txtExistencias.Text.Trim().Equals(""))
                {
                    throw new Exception("Se necesita agregar las existencias");
                }
                if (this.txtUsuario.Text.Trim().Equals(""))
                {
                    throw new Exception("Se necesita unidad de medida");
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

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                ValidacionesDatosRequeridos();
                this.conexion.guardarNuevoProducto(Convert.ToInt32(txtCodigo.Text),txtDescripcion.Text, Convert.ToDecimal(txtPrecioVenta.Text), seleccionarDescuento(cbxDescuentos.SelectedItem.ToString()),
                    valorImpuesto(cbImpuesto.SelectedItem.ToString()),cbxUnidadMedida.SelectedItem.ToString(), Convert.ToDecimal(txtPrecioCompra.Text),"Admin",Convert.ToInt32(txtExistencias.Text));
                MessageBox.Show("Producto agregado correctamente");
                limpiarCampos();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }//fin de metodo agregar productos

        public void limpiarCampos()
        {
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            cbxDescuentos.Text = null;
            cbImpuesto.Text = null;
            cbxUnidadMedida.Text = null;
            txtPrecioCompra.Text = "";
            txtUsuario.Text = "";
            txtExistencias.Text = "";
            txtPrecioVenta.Text = "";
        }


        public decimal seleccionarDescuento(string descuento)
        {
            decimal valorDescuento = 0.0m;

            if (descuento.Equals("5%"))
            {
                valorDescuento = 0.5m;
            }
            else if(descuento.Equals("10%"))
            {
                valorDescuento = 0.10m;
            }
            else if (descuento.Equals("15%"))
            {
                valorDescuento = 0.15m;
            }
            else if (descuento.Equals("25%"))
            {
                valorDescuento = 0.25m;
            }
            return valorDescuento;

        }//fin del metodo seleccionar descuento\\

        public decimal valorImpuesto(string valor)
        {
            decimal valorImpu = 0.13m;
            if (valor.Equals("No"))
            {
                valorImpu = 0.0m;
            }

            return valorImpu;
        }//fin de el metodo que decide si aplica o no el impuesto\\

    }
}
