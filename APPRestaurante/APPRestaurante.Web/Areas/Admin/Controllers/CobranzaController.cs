using APPRestaurante.UnitOfWork;
using APPRestaurante.Web.Areas.Admin.Filters;
using APPRestaurante.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    [Autenticado]
    public class CobranzaController : BaseController
    {
        public CobranzaController(IUnitOfWork unit) : base(unit)
        {
        }

        // GET: Admin/Cobranza
        public ActionResult Index()
        {
            var listaPedido = _unit.Pedido.GetAll().ToList();
            var result = new List<PedidoModel>();

            foreach (var item in listaPedido)
            {
                result.Add(new PedidoModel {
                    id = item.id,
                    fecha = String.Format("{0:MM/dd/yyyy}", item.fecha),
                    tipoPago = item.tipoPago,
                    mesa = item.mesa,
                    total = string.Format("{0:#,##0.00}", item.total),
                    estado = item.estado
                });
            }

            return View(result);
        }
    }
}