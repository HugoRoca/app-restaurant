using APPRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.Repository.Interfaces
{
    public interface IUsuarioRepository: IRepository<Usuario>
    {
        Usuario ValidarUsuario(string usuario, string clave);
    }
}
