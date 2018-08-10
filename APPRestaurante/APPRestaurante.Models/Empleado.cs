using Dapper.Contrib.Extensions;

namespace APPRestaurante.Models
{
    [Table("Empleado")]
    public class Empleado
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string celular { get; set; }
        public string tipoDocumento { get; set; }
        public string documento { get; set; }
        public string foto { get; set; }
        public string estado { get; set; }
    }
}
