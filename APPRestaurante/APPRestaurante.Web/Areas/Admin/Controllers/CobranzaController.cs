using APPRestaurante.Helper;
using APPRestaurante.Models;
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
                    estado = item.estado,
                    nombres = item.nombres
                });
            }

            return View(result);
        }

        public ActionResult Editar(int id = 0)
        {
            var result = new CobranzaModel();
            if (id > 0)
            {
                var pedido = _unit.Pedido.GetEntitybyId(id);

                result.cobranzaDetalles = _unit.Pedido.detalleCobranzaResults(pedido.id);
                result.fecha = pedido.fecha;
                result.fechaString = String.Format("{0:MM/dd/yyyy}", pedido.fecha);
                result.id = pedido.id;
                result.idEmpleado = pedido.idEmpleado;
                result.idUsuario = pedido.idUsuario;
                result.mesa = pedido.mesa;
                result.mesaString = "Mesa " + pedido.mesa;
                result.nombres = pedido.nombres;
                result.tipoPago = pedido.tipoPago;
                result.total = pedido.total;
                result.totalString = string.Format("{0:#,##0.00}", pedido.total);
                result.estado = pedido.estado;
            }
            return View(result);
        }

        public JsonResult Insertar(Pedido pedido)
        {
            pedido.idEmpleado = SessionHelper.GetUser();
            pedido.idUsuario = SessionHelper.GetUser();
            return Json(_unit.Pedido.Update(pedido));
        }
    }
}