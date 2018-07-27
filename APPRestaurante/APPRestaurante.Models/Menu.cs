using System.ComponentModel.DataAnnotations;

namespace APPRestaurante.Models
{
    public class Menu
    {
        public int id { get; set; }
        [Required]
        public string fecha { get; set; }
        public string usuario { get; set; }
        [Required]
        public string titulo { get; set; }
        [Required]
        public string descripcion { get; set; }
        [Required]
        public string tipo { get; set; }
        [Required]
        public double precio { get; set; }
        [Required]
        public string foto { get; set; }
        [Required]
        public int idUsuario { get; set; }
    }
}
