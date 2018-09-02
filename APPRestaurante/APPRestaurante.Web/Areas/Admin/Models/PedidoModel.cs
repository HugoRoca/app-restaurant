using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APPRestaurante.Web.Areas.Admin.Models
{
    public class PedidoModel
    {
        public int id { get; set; }
        public string fecha { get; set; }
        public string total { get; set; }
        public string nombres { get; set; }
        public int mesa { get; set; }
        public string tipoPago { get; set; }
        public int estado { get; set; }
    }
}