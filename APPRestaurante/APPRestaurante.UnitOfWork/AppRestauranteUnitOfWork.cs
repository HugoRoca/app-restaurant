using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPRestaurante.Models;
using APPRestaurante.Repository;

namespace APPRestaurante.UnitOfWork
{
    public class AppRestauranteUnitOfWork : IUnitOfWork, IDisposable
    {
        public AppRestauranteUnitOfWork()
        {
            Clientes = new BaseRepository<Cliente>();
        }

        public IRepository<Cliente> Clientes { get; private set; }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
