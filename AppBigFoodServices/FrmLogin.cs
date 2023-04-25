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
    public partial class FrmLogin : Form
    {
        private Conexion conexion;

        public FrmLogin()
        {
            InitializeComponent();
            this.conexion = new Conexion(FrmPrincipal.ObtenerStringConexion());
        }
        private void ValidacionesDatosRequeridos()
        {
            try
            {
                if (this.txtLogin.Text.Trim().Equals(""))
                {
                    throw new Exception("Se necisita ingresar un login");
                }
                if (this.txtPassword.Text.Trim().Equals(""))
                {
                    throw new Exception("Se necesita ingresar un password");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool IntentoAutenticacion(Usuario temp)
        {
            bool autenticado = false;
            
            if (this.conexion.autenticacion(temp))
            {
                autenticado = true;
            }
            return autenticado;
        }

        private void btnAutenticar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidacionesDatosRequeridos();
                Usuario temp = new Usuario();

                // Se rellenan los datos del objeto con los valores escritos por el usuario
                temp.Login = this.txtLogin.Text.Trim();
                temp.Password = this.txtPassword.Text.Trim();
                //
                if (this.IntentoAutenticacion(temp))
                {
                    this.Close();
                }
                else
                {
                    throw new Exception("Usuario o Password incorrectos.!!!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
