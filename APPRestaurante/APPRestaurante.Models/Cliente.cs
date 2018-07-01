using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace APPRestaurante.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public bool estado { get; set; }
    }
}
