using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.Models
{
    [Table("Rol")]
    public class Rol
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public int estado { get; set; }
    }
}
