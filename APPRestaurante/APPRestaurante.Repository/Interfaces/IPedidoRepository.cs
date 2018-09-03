using APPRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPRestaurante.Repository.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        IEnumerable<Pedido> lista();
        bool PedidoyDetallePedido(int mesa, IEnumerable<PedidoDetalle> items);
        IEnumerable<DetalleCobranzaResult> detalleCobranzaResults(int id);
    }
}
