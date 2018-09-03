using APPRestaurante.UnitOfWork;
using APPRestaurante.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APPRestaurante.Web.Areas.Admin.Filters;
using APPRestaurante.Helper;
using APPRestaurante.Web.Areas.Admin.Models;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    [Autenticado]
    public class InicioController : BaseController
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        public InicioController(IUnitOfWork unit) : base(unit)
        {
        }

        // GET: Admin/Inicio
        public ActionResult Index()
        {
            var result = new InicioModel();
            result.loMasPedido = _unit.Pedido.LoMasPedido();

            result.Pedidos = _unit.Pedido.PedidosPorFecha();

            result.totalPedidos = result.Pedidos.Count();
            result.totalPedidosPagados = result.Pedidos.Where(x => x.estado == 1).Count();
            result.totalPedidosProceso = result.Pedidos.Where(x => x.estado > 1).Count();
            var suma = result.Pedidos.Where(x => x.estado > 1).Sum(x => x.total);
            result.ventasDia = string.Format("{0:#,##0.00}", suma);

            var todoslospedidos = _unit.Pedido.GetAll();
            var sumaTodos = todoslospedidos.Where(x => x.estado > 1).Sum(x => x.total);
            result.ventaToal = string.Format("{0:#,##0.00}", sumaTodos);
            result.totalPedidoGeneral = todoslospedidos.Where(x => x.estado > 1).Count();


            return View(result);
        }
    }
}