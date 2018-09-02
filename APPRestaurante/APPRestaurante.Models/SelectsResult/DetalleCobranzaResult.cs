using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.Models
{
    public class DetalleCobranzaResult
    {
        public string titulo { get; set; }
        public string tipo { get; set; }
        public string precio { get; set; }
        public int cantidad { get; set; }
        public string subtotal { get; set; }
        public string foto { get; set; }
    }
}
