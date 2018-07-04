using APPRestaurante.Models;
using APPRestaurante.Repository;
using APPRestaurante.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Cliente> Clientes { get; }
        IUsuarioRepository Usuario { get; }
    }
}
