using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Models
{
    public class UsuarioModel
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public bool estado { get; set; }
        public string nombres { get; set; }
        public string foto { get; set; }
        public string rol { get; set; }
        public int idEmpleado { get; set; }
        public List<SelectListItem> empleados { get; set; }
    }
}