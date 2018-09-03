using APPRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APPRestaurante.Web.Areas.Admin.Models
{
    public class InicioModel
    {
        public IEnumerable<DetalleCobranzaResult> loMasPedido;
        public IEnumerable<Pedido> Pedidos { get; set; }
        public int totalPedidos { get; set; }
        public int totalPedidosPagados { get; set; }
        public int totalPedidosProceso { get; set; }
        public string ventasDia { get; set; }
        public string ventaToal { get; set; }
        public int totalPedidoGeneral { get; set; }
    }
}