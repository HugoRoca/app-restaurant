using APPRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.Repository.Interfaces
{
    public interface IMenuRepository
    {
        IEnumerable<Menu> ListaMenuPaginacion(DateTime desde, DateTime hasta, int startRow, int endRow);
    }
}
