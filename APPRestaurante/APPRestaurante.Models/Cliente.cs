using Dapper.Contrib.Extensions;

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
