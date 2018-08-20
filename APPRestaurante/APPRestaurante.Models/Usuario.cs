using Dapper.Contrib.Extensions;

namespace APPRestaurante.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public bool estado { get; set; }
        public string nombres { get; set; }
        public string foto { get; set; }
        public string rol { get; set; }
    }
}
