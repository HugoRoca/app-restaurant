using APPRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.Repository
{
    interface IRepository
    {
        int Insert(Cliente cliente);
        bool Update(Cliente cliente);
        bool Delete(Cliente cliente);
        Cliente getClientePorId(int idCliente);
        IEnumerable<Cliente> Lista();
    }
}
