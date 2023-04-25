using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Cuentas
    {
        public int numFactura { get; set; }
        public string codCliente { get; set; }
        public DateTime fechaFactura { get; set; }
        public DateTime fechaRegistro { get; set; }
        public decimal montoFactura { get; set; }
        public string usuario { get; set; }
        public char estado { get; set; }
    }
}
