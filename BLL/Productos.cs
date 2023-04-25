using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Productos
    {
        public int codigoInterno { get; set; }
        public int codigoBarra { get; set; }
        public string descripcion { get; set; }
        public decimal precioVenta { get; set; }
        public decimal descuento { get; set; }
        public decimal impuesto { get; set; }
        public string unidadMedida { get; set; }
        public decimal precioCompra { get; set; }
        public string usuario { get; set; }
        public int existencia { get; set; }
    }
}
