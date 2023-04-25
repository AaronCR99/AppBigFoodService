using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Factura
    {
        public string codCliente { get; set; }
        public decimal subTotal { get; set; }
        public decimal montoDescuento { get; set; }
        public decimal montoImpuesto { get; set; }
        public decimal total { get; set; }
        public char estado { get; set; }
        public string usuario { get; set; }
        public string tipoPago { get; set; }
        public string condicion { get; set; }
    }
}
