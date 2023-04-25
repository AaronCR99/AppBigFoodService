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
    public partial class FrmNuevoUsuario : Form
    {
        private Usuario usuario;
        private Conexion conexion;
        public FrmNuevoUsuario()
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
                    throw new Exception("No se permite agreagar sin un Login");
                }
                if (this.txtLogin.Text.Trim().Equals(""))
                {
                    throw new Exception("No se permite agreagar sin completar el Nombre");
                }
                if (this.txtPassword.Text.Trim().Equals(""))
                {
                    throw new Exception("No se permite agreagar sin el email");
                }
                if (this.txtConfirmarP.Text.Trim().Equals(""))
                {
                    throw new Exception("No se permite el password en blanco");
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

        public void CrearObjetoUsuario()
        {
            try
            {
                // aqui validamos los datos requeridos
                this.ValidacionesDatosRequeridos();

                // se crea la instancia del objeto usuario
                this.usuario = new Usuario();

                // se rellenan los datos del objeto con los valores escritos a nivel del font-end
                this.usuario.Login = this.txtLogin.Text.Trim();
                this.usuario.Password = this.txtPassword.Text.Trim();

                // Aqui validamos la confirmacion de los passwords
                if (!this.usuario.ConfirmarPassword(this.txtConfirmarP.Text.Trim()))
                {
                    throw new Exception("La confirmacion del Password es incorrecta");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }// Fin del metodo CrearObjetoUsuario

        private void btnAgregarUser_Click(object sender, EventArgs e)
        {
            try
            {
                // aqui se llama el metodo encargado de crear la instancia del objeto usuario
                this.CrearObjetoUsuario();
                // aqui se llama el metodo encargado de registrar el usuario en la base de datos
                this.RegistrarUsuario();
                MessageBox.Show("Usuario registrado correctamente.", "Proceso aplicado", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//Fin del metodo agregar Usuario

        private void RegistrarUsuario()
        {
            try
            {
                // Aqui utilizamos la capa DAL que contiene el objeto conexion
                // donde utilizamos el metodo RegistrarUsuario() enviando la instancia del objeto
                // que deseamos guardar 
                this.conexion.RegistrarUsuario(this.usuario);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }// Fin de RegistrarUsuario
    }
}
