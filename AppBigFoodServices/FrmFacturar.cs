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
using BLL;

using System.Transactions;

using AppBigFoodServices.Properties;

namespace AppBigFoodServices
{
    public partial class FrmFacturar : Form
    {
        // Variable para manejar la referencia de la capa acceso a datos
        private Conexion conexion;
        private Cliente cliente;
        private Productos producto;
        private Factura factura;
        private Detalle_Factura detalle;
        private Email email;

        string cedulaCliente = null;
        string codigoBarra = null;

        public FrmFacturar()
        {
            InitializeComponent();
            this.conexion = new Conexion(FrmPrincipal.ObtenerStringConexion());
        }

        private void ValidacionesDatosRequeridos()
        {
            try
            {
                if (this.txtNombreCompleto.Text.Trim().Equals(""))
                {
                    throw new Exception("No se permite el nombre completo en blanco");
                }
                if (this.txtCedulaLegal.Text.Trim().Equals(""))
                {
                    throw new Exception("No se permite la cedula legal en blanco");
                }
                if (this.cbxTipoPago.Text.Trim().Equals(""))
                {
                    throw new Exception("No se permite el tipo de pago en blanco");
                }
                if (this.cbxCondicion.Text.Trim().Equals(""))
                {
                    throw new Exception("No se permite la condicion en blanco");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
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

        private void txtDescripcionProducto_TextChanged(object sender, EventArgs e)
        {
            // Try para manejar un posible error durante el proceso de busqueda
            try
            {
                // se llama al metedo para realizr la busqueda
                this.BuscarProductoPorDescripcion(this.txtDescripcionProducto.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDatosProductos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            seleccionarProducto();
            InsertarDatosDescripcion();
        }

        private void dtgDatosCliente_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            seleccionarCliente();
            InsertarDatosCliente();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblFechaActual.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (producto.existencia < Convert.ToInt32(txtCantidad.Text))
            {
                llenarTable();
                txtDescripcion.Text = "";
                txtCantidad.Text = "";
                this.lblTotalPagar.Text = "¢ " + this.CalculoTotal().ToString();

            }
            else
            {
                MessageBox.Show("No se puede agregar mas  cantidad que las existencias" ,"Proceso Incorrecto!", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
            }
            }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            FrmAgregarCliente frm = new FrmAgregarCliente();
            frm.ShowDialog();
            frm.Close();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.eliminarProductoLista();
            this.lblTotalPagar.Text = "¢ " + this.CalculoTotal().ToString();
        }


        private void btnRealizarCompra_Click(object sender, EventArgs e)
        {
            this.transaccion();
        }


        //---------------------------- Metodos necesarios ---------------------------------\\
        private void BuscarClientePorNombre(string pNombre)
        {
            try // Se maneja un try para controlar un posible error durante el proceso de utilizacion
                // del metodo de busqueda.
            {
                this.dtgDatosCliente.DataSource = this.conexion.BuscarClientes(pNombre).Tables[0];
                this.dtgDatosCliente.AutoResizeColumns();
                this.dtgDatosCliente.ReadOnly = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }//fin del metodo de buscar clientes por nombre

        private void BuscarProductoPorDescripcion(string pDescripcion)
        {
            try // Se maneja un try para controlar un posible error durante el proceso de utilizacion
                // del metodo de busqueda.
            {
                this.dtgDatosProductos.DataSource = this.conexion.BuscarProductos(pDescripcion).Tables[0];
                this.dtgDatosProductos.AutoResizeColumns();
                this.dtgDatosProductos.ReadOnly = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }//fin del metodo de buscar clientes por nombre

        public void seleccionarCliente()
        {
            if (this.dtgDatosCliente.Rows.Count > 0) // Se verifica que el datagrid contenga informacion
            {
                if (this.dtgDatosCliente.SelectedCells.Count > 0) // Se verifica que el usuario seleccione una celda
                {
                    cedulaCliente = this.dtgDatosCliente.Rows[this.dtgDatosCliente.SelectedCells[0].RowIndex]
                       .Cells["cedulaLegal"].Value.ToString(); // Se toma el codigo del cliente
                }
                else
                {
                    throw new Exception("Seleccione la celda del cliente que desea eliminar");
                }
            }
            else
            {
                throw new Exception("Consulte los datos del usuario a eliminar");
            }
        }//fin del metodo seleccionar cliente

        public void seleccionarProducto()
        {
            if (this.dtgDatosProductos.Rows.Count > 0) // Se verifica que el datagrid contenga informacion
            {
                if (this.dtgDatosProductos.SelectedCells.Count > 0) // Se verifica que el usuario seleccione una celda
                {
                    codigoBarra = this.dtgDatosProductos.Rows[this.dtgDatosProductos.SelectedCells[0].RowIndex]
                       .Cells["codigoBarra"].Value.ToString(); // Se toma el codigo del cliente
                }
                else
                {
                    throw new Exception("Seleccione la celda del cliente que desea eliminar");
                }
            }
            else
            {
                throw new Exception("Consulte los datos del usuario a eliminar");
            }
        }//fin del metodo seleccionar cliente


        //Metodo encargado de llenar los txt de cedula y nombre con los valores seleccionados
        public void InsertarDatosCliente()
        {
            try
            {
                this.cliente = this.conexion.consultarCliente(cedulaCliente);
                if (this.cliente != null)
                {
                    //se rellena el fron-end con los datos del objeto
                    this.txtCedulaLegal.Text = this.cliente.cedulaLegal;
                    this.txtNombreCompleto.Text = this.cliente.nombreCompleto;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }// fin del metodo de consulta cliente

        public void InsertarDatosDescripcion()
        {
            try
            {
                this.producto = this.conexion.consultarProductos(codigoBarra);
                if (this.producto != null)
                {
                    //se rellena el fron-end con los datos del objeto
                    this.txtDescripcion.Text = this.producto.descripcion;
                }
            }
            catch (Exception)
            {
                throw;
            }//fin del catch
        }// fin del metodo de consulta cliente

        private void llenarTable()
        {
            try //manejamos un try para controlar  un posible error durante el proceso de utilización
                //del método de llenar tabla.
            {
                ListaProductos listaProductos = new ListaProductos();
                decimal descuentoProducto = this.CalcularDescuento(producto.descuento, this.CalculoSubTotal(producto.precioVenta, Convert.ToInt32(txtCantidad.Text)));
                decimal subtotal = this.CalculoSubTotal(producto.precioVenta, Convert.ToInt32(txtCantidad.Text));

                if (producto != null && txtCantidad.Text != "")
                {
                    listaProductos.Codigo = producto.codigoBarra;
                    listaProductos.Nombre = producto.descripcion;
                    listaProductos.Cantidad = Convert.ToInt32(txtCantidad.Text.Trim());
                    listaProductos.Descuento = descuentoProducto;

                    listaProductos.Impuesto = ((subtotal - descuentoProducto) * 0.13m);
                    listaProductos.Subtotal = this.CalculoSubTotal(producto.precioVenta, Convert.ToInt32(txtCantidad.Text));
                    listaProductos.Total = this.CalculoTotalProducto(producto.precioVenta, Convert.ToInt32(txtCantidad.Text),descuentoProducto,producto.impuesto);
                    listaProductos.CodigoInterno = producto.codigoInterno;
                    listaProductos.PrecioUnitario = producto.precioVenta;

                    this.dtgProductosAgregados.Rows.Add();
                    this.dtgProductosAgregados.Rows[this.dtgProductosAgregados.Rows.Count - 1].Cells["Codigo"].Value = listaProductos.Codigo;
                    this.dtgProductosAgregados.Rows[this.dtgProductosAgregados.Rows.Count - 1].Cells["Nombre"].Value = listaProductos.Nombre;
                    this.dtgProductosAgregados.Rows[this.dtgProductosAgregados.Rows.Count - 1].Cells["Cantidad"].Value = listaProductos.Cantidad;
                    this.dtgProductosAgregados.Rows[this.dtgProductosAgregados.Rows.Count - 1].Cells["Descuento"].Value = listaProductos.Descuento;
                    this.dtgProductosAgregados.Rows[this.dtgProductosAgregados.Rows.Count - 1].Cells["Impuesto"].Value = listaProductos.Impuesto;
                    this.dtgProductosAgregados.Rows[this.dtgProductosAgregados.Rows.Count - 1].Cells["Subtotal"].Value = listaProductos.Subtotal;
                    this.dtgProductosAgregados.Rows[this.dtgProductosAgregados.Rows.Count - 1].Cells["Total"].Value = listaProductos.Total;
                    this.dtgProductosAgregados.Rows[this.dtgProductosAgregados.Rows.Count - 1].Cells["CodigoInterno"].Value = listaProductos.CodigoInterno;
                    this.dtgProductosAgregados.Rows[this.dtgProductosAgregados.Rows.Count - 1].Cells["PrecioUnitario"].Value = listaProductos.PrecioUnitario;
                    this.dtgProductosAgregados.AutoResizeColumns();
                    this.dtgProductosAgregados.ReadOnly = true;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message +" Revisar que los datos de nombre del producto y la cantidad se encuentren llenos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fin del metodo buscarRepuesto

        private void eliminarProductoLista()
        {
                if (this.dtgProductosAgregados.Rows.Count > 0) // Se verifica que el datagrid contenga informacion
                {
                    if (this.dtgProductosAgregados.SelectedCells.Count > 0) // Se verifica que el usuario seleccione una celda
                    {
                        dtgProductosAgregados.Rows.RemoveAt(dtgProductosAgregados.CurrentRow.Index); 
                    }
                    else
                    {
                        throw new Exception("Seleccione la celda del producto que desea eliminar");
                    }
                }
                else
                {
                MessageBox.Show("Consulte los datos del producto a eliminar","Proceso de seleccion incorrecto", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
            
        } // Fin de eliminarUsuario //




        // ---------- Metodos encargados de realizar los calculos de Subtotal, impuesto y Total -----------//
        public decimal CalculoSubTotal(decimal precioP, int cantidad)
        {
            decimal subTotal = 0.0m;
            subTotal = (precioP * cantidad);

            return subTotal;
        }// Cierre de CalculoSubTotal //
        //******************************************//

        

        public decimal CalculoTotalProducto(decimal precioP, int cantidad, decimal descuento, decimal impuesto)
        {
            decimal total = 0.0m;
            decimal valorImpuesto = 0.0m;

            total = (precioP * cantidad)- descuento;
            valorImpuesto = this.CalculoImpuesto(total, impuesto);
            total = total + valorImpuesto;

            return total;
        }// Cierre de CalculoSubTotal //
        //******************************************//
        public decimal CalculoImpuesto(decimal subTotal, decimal impuesto)
        {
            decimal montoImpuestoIVA = 0.0m;
            montoImpuestoIVA = subTotal * impuesto;

            return montoImpuestoIVA;
        }// Cierre del CalculoImpuesto //
        //****************************************//

        public decimal CalcularDescuento(decimal decuento, decimal subtotal)
        {
            decimal totalDescuento = 0.0m;

            totalDescuento = subtotal * decuento;

            return totalDescuento;
        }//fin de calcular descuento

        public decimal CalculoTotal()
        {
            decimal totalPagar = 0.0m;

            foreach (DataGridViewRow item in dtgProductosAgregados.Rows)
            {
                totalPagar += Convert.ToDecimal(item.Cells[6].Value.ToString());
            }

            //Hay que sumar el impuesto\\
            return totalPagar;
        }// Cierre del CalculoTotal //


        private void transaccion()
        {
            try
            {
                this.ValidacionesDatosRequeridos();
                var opciones = new TransactionOptions();
                opciones.IsolationLevel = System.Transactions.IsolationLevel.Serializable;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, opciones))
                {
                    this.llenarFactura();
                    this.conexion.guardarDatosFactura(factura);
                    List<Factura> lstFactura = new List<Factura>();
                    List<Detalle_Factura> lstDetalleFactura = new List<Detalle_Factura>();

                    lstFactura.Add(factura); 

                    foreach (DataGridViewRow item in dtgProductosAgregados.Rows)
                    {
                        detalle = new Detalle_Factura();

                        detalle.codInterno = Convert.ToInt32(item.Cells[7].Value.ToString());
                        detalle.cantidad = Convert.ToInt32(item.Cells[2].Value.ToString());
                        detalle.precioUnitario = Convert.ToDecimal(item.Cells[8].Value.ToString());
                        detalle.subTotal = Convert.ToDecimal(item.Cells[5].Value.ToString());
                        detalle.porImp = Convert.ToDecimal(item.Cells[4].Value.ToString());
                        detalle.porDescuento = Convert.ToDecimal(item.Cells[3].Value.ToString());
                        detalle.nombreProducto = item.Cells[1].Value.ToString();

                        lstDetalleFactura.Add(detalle);
                        this.conexion.guardarDatosDetalleFactura(detalle);
                    }

                    if (cbxCondicion.SelectedItem.ToString().Equals("Credito"))
                    {
                        this.conexion.guardarDatosCuentasCobrar(cliente.cedulaLegal, factura.total, "Admin", 'A');
                    }

                    foreach (var item in lstDetalleFactura)
                    {
                        this.conexion.manejoExistencias(item.cantidad, item.codInterno);
                    }
                    this.limpiarCampos();
                    this.email = new Email();
                    this.email.enviar(lstFactura, lstDetalleFactura,cliente.email,cliente.nombreCompleto);
                    scope.Complete();
                    MessageBox.Show("Transaccion realizada con exito.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }// fin del metodo transaccion


        public void llenarFactura()
        {
            factura = new Factura();

            factura.codCliente = txtCedulaLegal.Text;
            factura.subTotal = CalculoSubTotalFact();
            factura.montoDescuento =CalculoDescuentoFact() ;
            factura.montoImpuesto = CalculoImpuestoFact();
            factura.total = CalculoTotal();
            factura.estado = 'A';
            factura.usuario = "Admin";
            factura.tipoPago = cbxTipoPago.SelectedItem.ToString();
            factura.condicion = cbxCondicion.SelectedItem.ToString();
            
        }//fin del metetdo llenar factura\\

        public decimal CalculoSubTotalFact()
        {
            decimal subTotal = 0.0m;

            foreach (DataGridViewRow valor in dtgProductosAgregados.Rows)
            {
                subTotal += Convert.ToDecimal(valor.Cells[5].Value.ToString());
            }
            return subTotal;
        } // Cierre del CalculoSubTotalFact //

        public decimal CalculoImpuestoFact()
        {
            decimal impuestoFact = 0.0m;

            foreach (DataGridViewRow valor in dtgProductosAgregados.Rows)
            {
                impuestoFact += Convert.ToDecimal(valor.Cells[4].Value.ToString());
            }
            return impuestoFact;
        } // Cierre del CalculoImpuestoFact //

        public decimal CalculoDescuentoFact()
        {
            decimal descuentoFact = 0.0m;

            foreach (DataGridViewRow valor in dtgProductosAgregados.Rows)
            {
                descuentoFact += Convert.ToDecimal(valor.Cells[3].Value.ToString());
            }
            return descuentoFact;
        } // Cierre del CalculoDescuentoFact //

        public void limpiarCampos()
        {
            txtCantidad.Text = "";
            txtDescripcionProducto.Text = "";
            txtDescripcion.Text = "";
            txtNombreCliente.Text = "";
            txtNombreCompleto.Text = "";
            txtCedulaLegal.Text = "";
            cbxCondicion.Text = null;
            cbxTipoPago.Text = null;
            dtgProductosAgregados.Rows.Clear();
        }//fin del metodo limpiar

     
    }
}
