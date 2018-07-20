using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.Models
{
    public class Menu
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string usuario { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public double precio { get; set; }
        public string foto { get; set; }
    }
}
