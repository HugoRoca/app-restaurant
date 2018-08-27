using APPRestaurante.Models;
using APPRestaurante.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.Repository.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        public bool PedidoyDetallePedido(Pedido pedido, IEnumerable<PedidoDetalle> items)
        {
            throw new NotImplementedException();
        }
    }
}
