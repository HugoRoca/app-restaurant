using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APPRestaurante.Web.Areas.Admin.Models
{
    public class CobranzaModel
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public double total { get; set; }
        public string totalString { get; set; }
        public string nombres { get; set; }
        public int mesa { get; set; }
        public string tipoPago { get; set; }
        public int idEmpleado { get; set; }
        public int idUsuario { get; set; }
        public int estado { get; set; }
        public IEnumerable<PedidoDetalleModel> pedidoDetalles { get; set; }
    }
}