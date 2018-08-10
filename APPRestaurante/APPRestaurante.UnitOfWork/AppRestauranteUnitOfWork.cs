using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPRestaurante.Models;
using APPRestaurante.Repository;
using APPRestaurante.Repository.Interfaces;
using APPRestaurante.Repository.Repositories;

namespace APPRestaurante.UnitOfWork
{
    public class AppRestauranteUnitOfWork : IUnitOfWork, IDisposable
    {
        public AppRestauranteUnitOfWork()
        {
            Clientes = new BaseRepository<Cliente>();
            Usuario = new UsuarioRepository();
            Permiso = new PermisoRepository();
            Menu = new MenuRepository();
            Empleado = new BaseRepository<Empleado>();
        }

        public IRepository<Cliente> Clientes { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }
        public IPermisoRepository Permiso { get; private set; }
        public IMenuRepository Menu { get; private set; }
        public IRepository<Empleado> Empleado {get; private set;}

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
