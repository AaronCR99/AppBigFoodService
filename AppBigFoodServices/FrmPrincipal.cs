using AppBigFoodServices.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBigFoodServices
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }


        //------------------------- Acciones de los botones ------------------------------\\
        private void btnFacturar_Click(object sender, EventArgs e)
        {
            // se declara una variable de tipo formulario y se asigna una instancia del formulario
            FrmFacturar frm = new FrmFacturar();
            // mostramos el formulario
            frm.ShowDialog();
            // aqui liberamos los recursos en memoria
            frm.Close();
        }

        private void btnBuscarClientes_Click(object sender, EventArgs e)
        {
            // se declara una variable de tipo formulario y se asigna una instancia del formulario
            FrmListaClientes frm = new FrmListaClientes();
            // mostramos el formulario
            frm.ShowDialog();
            // aqui liberamos los recursos en memoria
            frm.Close();
        }

        private void agregarClienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // se declara una variable de tipo formulario y se asigna una instancia del formulario
            FrmAgregarCliente frm = new FrmAgregarCliente();
            // mostramos el formulario
            frm.ShowDialog();
            // aqui liberamos los recursos en memoria
            frm.Close();
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // se declara una variable de tipo formulario y se asigna una instancia del formulario
            FrmNuevoProducto frm = new FrmNuevoProducto();
            // mostramos el formulario
            frm.ShowDialog();
            // aqui liberamos los recursos en memoria
            frm.Close();
        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            // se declara una variable de tipo formulario y se asigna una instancia del formulario
            FrmNuevoUsuario frm = new FrmNuevoUsuario();
            // mostramos el formulario
            frm.ShowDialog();
            // aqui liberamos los recursos en memoria
            frm.Close();
        }

        private void cuentasPorCobrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // se declara una variable de tipo formulario y se asigna una instancia del formulario
            FrmCuentas frm = new FrmCuentas();
            // mostramos el formulario
            frm.ShowDialog();
            // aqui liberamos los recursos en memoria
            frm.Close();
        }

        //---------------------------- String de conexion ---------------------------------\\
        public static string ObtenerStringConexion()
        {
            return Settings.Default.StringConexion;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.lblFechaActual.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        //---------------------------- Metodos necesarios ---------------------------------\\

        //Método encargado de  llamar las aplicaciones
        private void EjecutarAplicaciones(string strApp)
        {
            System.Diagnostics.Process.Start(strApp);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblFechaActual.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            //this.notifyIcon1.ShowBalloonTip(25);

            // Se llama a nuestro formulario Login //
            this.mostrarPantallaLogin();
        }

        public void mostrarPantallaLogin()
        {
            // se declara una variable de tipo formulario y se asigna una instancia del formulario
            FrmLogin frm = new FrmLogin();
            // mostramos el formulario
            frm.ShowDialog();
            // aqui liberamos los recursos en memoria
            frm.Close();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void hojaDeCálculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.EjecutarAplicaciones("excel.exe");
        }

        private void documentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.EjecutarAplicaciones("winword.exe");
        }

        private void calculadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.EjecutarAplicaciones("calc.exe");
        }

        private void inicioDeSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            frm.ShowDialog();
            frm.Close();
        }
    }
}
