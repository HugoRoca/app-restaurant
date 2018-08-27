using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.Models
{
    public class PedidoDetalle
    {
        public int id { get; set; }
        public int idPedido { get; set; }
        public int idMenu { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public double subtotal { get; set; }
    }
}
