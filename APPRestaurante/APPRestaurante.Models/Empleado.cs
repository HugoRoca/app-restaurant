using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.Models
{
    public class Empleado
    {
        public int idEmpleado { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string direccion { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public string foto { get; set; }
        public bool estado { get; set; }
        public int idRol { get; set; }
    }
}
