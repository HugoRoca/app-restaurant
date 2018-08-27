using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.Models
{
    [Table("[Pedido]")]
    public class Pedido
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public double total { get; set; }
        public string nombres { get; set; }
        public int mesa { get; set; }
        public string tipoPago { get; set; }
        public int idEmpleado { get; set; }
        public int idUsuario { get; set; }
        public int estado { get; set; }
        [Computed]
        public IEnumerable<PedidoDetalle> pedidoDetalles { get; set; }
    }
}
