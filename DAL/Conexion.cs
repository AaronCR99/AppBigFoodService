using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using BLL;
using System.Data;

namespace DAL
{
    public class Conexion
    {
        //Objetos para interactuar con la bd
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter dataAdapter;
        private SqlDataReader dataReader;

        // Editado el 7/7/2021 -- Seccion 27 --
        // Permite almacenar los datos que vienen de una tabla de sql
        private DataSet dataSet;

        // Variable para almacenar el string de conexion
        private string strConexion;

        //Constructor de la clase con parametros
        public Conexion(string strCnx)
        {
            this.strConexion = strCnx;
        }

        public DataSet BuscarClientes(string pNombre)
        {
            try
            {
                // se instancia una conexion
                this.connection = new SqlConnection(this.strConexion);

                // se intenta abrir
                this.connection.Open();

                // se instancia el comando
                this.command = new SqlCommand();

                // se asigna la conexion al nuevo comando
                this.command.Connection = this.connection;

                // se indica el tipo de comando
                this.command.CommandType = CommandType.StoredProcedure;

                // se indica el nombre del proceso almacenado
                this.command.CommandText = "[Sp_Cns_Cliente]";

                // asignamos el valor al parametro del procedimiento
                this.command.Parameters.AddWithValue("@Nombre", pNombre);

                // se instancia un adaptador
                this.dataAdapter = new SqlDataAdapter();

                // se asigna el comando al adaptador
                this.dataAdapter.SelectCommand = this.command;

                // se instacia un DataSet para guardar los datos
                this.dataSet = new DataSet();

                // se llena el data set con los datos del comando
                this.dataAdapter.Fill(this.dataSet);

                // se cierra los recursos
                this.connection.Close();

                // se liberan los recursos
                this.connection.Dispose();
                this.command.Dispose();
                this.dataAdapter.Dispose();

                // se retorna el DataSet
                return this.dataSet;
            }
            catch (Exception)
            {

                throw;
            }
        } // Fin de buscarUsuarios

        public DataSet BuscarProductos(string pDescripcion)
        {
            try
            {
                // se instancia una conexion
                this.connection = new SqlConnection(this.strConexion);

                // se intenta abrir
                this.connection.Open();

                // se instancia el comando
                this.command = new SqlCommand();

                // se asigna la conexion al nuevo comando
                this.command.Connection = this.connection;

                // se indica el tipo de comando
                this.command.CommandType = CommandType.StoredProcedure;

                // se indica el nombre del proceso almacenado
                this.command.CommandText = "[Sp_Cns_Productos]";

                // asignamos el valor al parametro del procedimiento
                this.command.Parameters.AddWithValue("@descripcion", pDescripcion);

                // se instancia un adaptador
                this.dataAdapter = new SqlDataAdapter();

                // se asigna el comando al adaptador
                this.dataAdapter.SelectCommand = this.command;

                // se instacia un DataSet para guardar los datos
                this.dataSet = new DataSet();

                // se llena el data set con los datos del comando
                this.dataAdapter.Fill(this.dataSet);

                // se cierra los recursos
                this.connection.Close();

                // se liberan los recursos
                this.connection.Dispose();
                this.command.Dispose();
                this.dataAdapter.Dispose();

                // se retorna el DataSet
                return this.dataSet;
            }
            catch (Exception)
            {

                throw;
            }
        } // Fin de buscar producto

        public DataSet BuscarCuentas(string pCedula)
        {
            try
            {
                // se instancia una conexion
                this.connection = new SqlConnection(this.strConexion);

                // se intenta abrir
                this.connection.Open();

                // se instancia el comando
                this.command = new SqlCommand();

                // se asigna la conexion al nuevo comando
                this.command.Connection = this.connection;

                // se indica el tipo de comando
                this.command.CommandType = CommandType.StoredProcedure;

                // se indica el nombre del proceso almacenado
                this.command.CommandText = "[Sp_Cns_Cuentas]";

                // asignamos el valor al parametro del procedimiento
                this.command.Parameters.AddWithValue("@Cedula", pCedula);

                // se instancia un adaptador
                this.dataAdapter = new SqlDataAdapter();

                // se asigna el comando al adaptador
                this.dataAdapter.SelectCommand = this.command;

                // se instacia un DataSet para guardar los datos
                this.dataSet = new DataSet();

                // se llena el data set con los datos del comando
                this.dataAdapter.Fill(this.dataSet);

                // se cierra los recursos
                this.connection.Close();

                // se liberan los recursos
                this.connection.Dispose();
                this.command.Dispose();
                this.dataAdapter.Dispose();

                // se retorna el DataSet
                return this.dataSet;
            }
            catch (Exception)
            {

                throw;
            }
        } // Fin de buscar producto


        public Cliente consultarCliente(string pCedula)
        {
            try
            {
                Cliente temp = null;

                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Datos_Clientes]";
                // se asigna el valor al parametro
                this.command.Parameters.AddWithValue("@Cedula", pCedula);

                // muy importante el comando es de lectura ExecuteReader
                this.dataReader = this.command.ExecuteReader();

                // se pregunta si el lector presenta datos
                if (this.dataReader.Read())
                {
                    // Se crea una instancia del objeto usuario
                    temp = new Cliente();

                    // rellenamos el objeto con los datos que nos retorna el procedimiento almacenado
                    temp.cedulaLegal = this.dataReader.GetValue(0).ToString();
                    temp.tipoCedula = this.dataReader.GetValue(1).ToString();
                    temp.nombreCompleto = this.dataReader.GetValue(2).ToString();
                    temp.email = this.dataReader.GetValue(3).ToString();
                    temp.fechaRegistro = Convert.ToDateTime(this.dataReader.GetValue(4).ToString());
                    temp.estado = Convert.ToChar(this.dataReader.GetValue(5).ToString());
                    temp.usuario = this.dataReader.GetValue(6).ToString();
                }

                // Cerramos la conexion
                this.connection.Close();

                // Liberacion de recursos
                this.connection.Dispose();
                this.command.Dispose();
                this.dataReader = null;

                // Se retorna el objeto usuario con los datos facilitados por el Stored Procedure
                return temp;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        } // Cierre del consultarCliente

        public Productos consultarProductos(string pCodigoBarra)
        {
            try
            {
                Productos temp = null;

                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Datos_Productos]";
                // se asigna el valor al parametro
                this.command.Parameters.AddWithValue("@Codigo", pCodigoBarra);

                // muy importante el comando es de lectura ExecuteReader
                this.dataReader = this.command.ExecuteReader();

                // se pregunta si el lector presenta datos
                if (this.dataReader.Read())
                {
                    // Se crea una instancia del objeto usuario
                    temp = new Productos();

                    // rellenamos el objeto con los datos que nos retorna el procedimiento almacenado
                    temp.codigoInterno = Convert.ToInt32(this.dataReader.GetValue(0).ToString());
                    temp.codigoBarra = Convert.ToInt32(this.dataReader.GetValue(1).ToString());
                    temp.descripcion = this.dataReader.GetValue(2).ToString();
                    temp.precioVenta = Convert.ToDecimal(this.dataReader.GetValue(3).ToString());
                    temp.descuento = Convert.ToDecimal(this.dataReader.GetValue(4).ToString());
                    temp.impuesto = Convert.ToDecimal(this.dataReader.GetValue(5).ToString());
                    temp.unidadMedida = this.dataReader.GetValue(6).ToString();
                    temp.precioCompra = Convert.ToDecimal(this.dataReader.GetValue(7).ToString());
                    temp.usuario = this.dataReader.GetValue(8).ToString();
                    temp.existencia = Convert.ToInt32(this.dataReader.GetValue(9).ToString());
                }

                // Cerramos la conexion
                this.connection.Close();

                // Liberacion de recursos
                this.connection.Dispose();
                this.command.Dispose();
                this.dataReader = null;

                // Se retorna el objeto usuario con los datos facilitados por el Stored Procedure
                return temp;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        } // Cierre del consultarProducto

        public void eliminarCliente(string pLogin)
        {
            try
            {
                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Del_Usuario]";

                this.command.Parameters.AddWithValue("@Login", pLogin);

                this.command.ExecuteNonQuery();
                this.connection.Close();
                this.command.Dispose();
                this.connection.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        } // Fin del Metodo EliminarUsuario //


        public void guardarNuevoCliente(string pCedulaLegal, string pTipoCedula, string pNombreCompleto, string pEmail, string pUsuario)
        {
            try
            {
                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Ins_Clientes]";
                // se asigna el valor al parametro
                this.command.Parameters.AddWithValue("@cedulaLegal", pCedulaLegal);
                this.command.Parameters.AddWithValue("@tipoCedula", pTipoCedula);
                this.command.Parameters.AddWithValue("@nombreCompleto", pNombreCompleto);
                this.command.Parameters.AddWithValue("@email", pEmail);
                this.command.Parameters.AddWithValue("@usuario", pUsuario);

                // muy importante el comando es de lectura ExecuteReader
                this.dataReader = this.command.ExecuteReader();

                // Cerramos la conexion
                this.connection.Close();

                // Liberacion de recursos
                this.connection.Dispose();
                this.command.Dispose();
                this.dataReader = null;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }//fin del metodo guardarDatosRvision

        public void guardarNuevoProducto(int pCodigoBarra, string pDescripcion, decimal pPrecioVenta, decimal pDescuento, decimal pImpuesto, 
            string UnidadMedida, decimal pPrecioCompra, string pUsuario, int pExistencias)
        {
            try
            {
                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Ins_Productos]";
                // se asigna el valor al parametro
                this.command.Parameters.AddWithValue("@codigoBarra", pCodigoBarra);
                this.command.Parameters.AddWithValue("@descripcion", pDescripcion);
                this.command.Parameters.AddWithValue("@precioVenta", pPrecioVenta);
                this.command.Parameters.AddWithValue("@descuento", pDescuento);
                this.command.Parameters.AddWithValue("@impuesto", pImpuesto);
                this.command.Parameters.AddWithValue("@unidadMedida", UnidadMedida);
                this.command.Parameters.AddWithValue("@precioCompra", pPrecioCompra);
                this.command.Parameters.AddWithValue("@usuario", pUsuario);
                this.command.Parameters.AddWithValue("@existencia", pExistencias);

                // muy importante el comando es de lectura ExecuteReader
                this.dataReader = this.command.ExecuteReader();

                // Cerramos la conexion
                this.connection.Close();

                // Liberacion de recursos
                this.connection.Dispose();
                this.command.Dispose();
                this.dataReader = null;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }//fin del metodo guardarDatosRvision


        //Metodo encargado de autenticar los usuarios\\
        public bool autenticacion(Usuario pUser)
        {
            try
            {
                bool autorizado = false;

                // Se crea una instancia de la conexion
                this.connection = new SqlConnection(this.strConexion);

                // se intenta abrir la conexion
                this.connection.Open();

                // se instancia el comando
                this.command = new SqlCommand();

                // se asigna la conexion
                this.command.Connection = this.connection;

                // se indica el tipo de comando
                this.command.CommandType = System.Data.CommandType.StoredProcedure;

                // Se asigna el nombre del procedimiento almacenado
                this.command.CommandText = "[Sp_Cns_Usuario]";

                // Asignacion de los valores a cada parametro
                this.command.Parameters.AddWithValue("@Login", pUser.Login);
                this.command.Parameters.AddWithValue("@Password", pUser.Password);

                //Realizamos lectura de los datos del Usuario
                this.dataReader = this.command.ExecuteReader();

                // Preguntamos si existen datos
                if (this.dataReader.Read())
                {
                    // Aqui indicamos que el usuario esta autorizado
                    autorizado = true;
                }
                return autorizado;
            }
            catch (Exception )
            {
               
                throw;
            }
        }//Fin de metodo de autenticacion de login


        public void RegistrarUsuario(Usuario pUser)
        {
            try
            {
                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();
                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Ins_Usuarios]";

                // Se asignan los valores a cada parametro de nuestro procedimiento almacenado
                this.command.Parameters.AddWithValue("@Login", pUser.Login);
                this.command.Parameters.AddWithValue("@Password", pUser.Password);

                // Se ejecuta el comando
                this.command.ExecuteNonQuery();

                // Cierre de la conexion
                this.connection.Close();

                // Liberacion de los recursos
                this.connection.Dispose();
                this.command.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
        }// Fin de RegistrarUsuario //


        //MODIFICAR ESTOS METODOS PARA QUE LO QUE RESIVA SEAUN OBJETO DE TIPO FACTURA\\
        public void guardarDatosFactura(Factura factura)
        {
            try
            {
                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Ins_Facturas]";
                // se asigna el valor al parametro
                this.command.Parameters.AddWithValue("@codCliente", factura.codCliente);
                this.command.Parameters.AddWithValue("@subTotal", factura.subTotal);
                this.command.Parameters.AddWithValue("@montoDescuento", factura.montoDescuento);
                this.command.Parameters.AddWithValue("@montoImpuesto", factura.montoImpuesto);
                this.command.Parameters.AddWithValue("@total", factura.total);
                this.command.Parameters.AddWithValue("@estado", factura.estado);
                this.command.Parameters.AddWithValue("@usuario", factura.usuario);
                this.command.Parameters.AddWithValue("@tipoPago", factura.tipoPago);
                this.command.Parameters.AddWithValue("@condicion", factura.condicion);

                // muy importante el comando es de lectura ExecuteReader
                this.dataReader = this.command.ExecuteReader();

                // Cerramos la conexion
                this.connection.Close();

                // Liberacion de recursos
                this.connection.Dispose();
                this.command.Dispose();
                this.dataReader = null;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }//fin del metodo guardarDatosRvision


        public void guardarDatosDetalleFactura(Detalle_Factura detalle)
        {
            try
            {
                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Ins_DetFactura]";
                // se asigna el valor al parametro
                this.command.Parameters.AddWithValue("@codInterno", detalle.codInterno);
                this.command.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                this.command.Parameters.AddWithValue("@precioUnitario", detalle.precioUnitario);
                this.command.Parameters.AddWithValue("@subTotal", detalle.subTotal);
                this.command.Parameters.AddWithValue("@porImp", detalle.porImp);
                this.command.Parameters.AddWithValue("@porDescuento", detalle.porDescuento);
                this.command.Parameters.AddWithValue("@nombreProducto", detalle.nombreProducto);

                // muy importante el comando es de lectura ExecuteReader
                this.dataReader = this.command.ExecuteReader();

                // Cerramos la conexion
                this.connection.Close();

                // Liberacion de recursos
                this.connection.Dispose();
                this.command.Dispose();
                this.dataReader = null;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }//fin del metodo guardarDatosRvision



        public void guardarDatosCuentasCobrar(string pCodCliente, decimal pTotalFactura, string pUser, char pEstado)
        {
            try
            {
                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Ins_Cuentas]";
                // se asigna el valor al parametro
                this.command.Parameters.AddWithValue("@codCliente", pCodCliente);
                this.command.Parameters.AddWithValue("@totalFactura", pTotalFactura);
                this.command.Parameters.AddWithValue("@usuario", pUser);
                this.command.Parameters.AddWithValue("@estado", pEstado);

                // muy importante el comando es de lectura ExecuteReader
                this.dataReader = this.command.ExecuteReader();

                // Cerramos la conexion
                this.connection.Close();

                // Liberacion de recursos
                this.connection.Dispose();
                this.command.Dispose();
                this.dataReader = null;

                //↓↓↓ con esto  se realizan las puebas de concurrencia con el fin de que la transaccion ocurre por completo o no ocurra ↓↓↓
                //throw new Exception("Prueba error");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }//fin del metodo guardarDatosRvision

        public void manejoExistencias(int pCantidad, int pCodigoInterno)
        {
            try
            {
                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Existencias]";
                // se asigna el valor al parametro
                this.command.Parameters.AddWithValue("@codigoInterno", pCodigoInterno);
                this.command.Parameters.AddWithValue("@cantidad", pCantidad);
                // muy importante el comando es de lectura ExecuteReader
                this.dataReader = this.command.ExecuteReader();

                // Cerramos la conexion
                this.connection.Close();

                // Liberacion de recursos
                this.connection.Dispose();
                this.command.Dispose();
                this.dataReader = null;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }//fin del metodo manejo de existencias


    }
}
