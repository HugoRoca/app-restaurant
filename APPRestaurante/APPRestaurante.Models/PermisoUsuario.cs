using Dapper.Contrib.Extensions;

namespace APPRestaurante.Models
{
    [Table("PermisoUsuario")]
    public class PermisoUsuario
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public int idPermiso { get; set; }
        public bool estado { get; set; }
    }
}
