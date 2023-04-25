using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Usuario
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public bool ConfirmarPassword(string clave)
        {
            try
            {
                bool confirmado = false;

                // Se realiza una comparacion exacta
                if (this.Password.Trim().Equals(clave))
                {
                    confirmado = true;
                }
                // retorna un valor
                return confirmado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
