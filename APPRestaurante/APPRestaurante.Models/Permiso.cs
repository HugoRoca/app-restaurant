using Dapper.Contrib.Extensions;

namespace APPRestaurante.Models
{
    [Table("Permiso")]
    public class Permiso
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
        public int padre { get; set; }
        public string url { get; set; }
        public string icono { get; set; }
        public string controlador { get; set; }
        public bool active { get; set; }
    }
}
