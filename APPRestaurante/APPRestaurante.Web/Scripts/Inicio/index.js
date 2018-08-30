var homeModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;
    var listaData = globalData.listaDatos;

    me.Servicios = (function () {

    })();

    me.Eventos = (function () {
        function BuscarPedidoEnListaGeneral(_id) {
            for (var i in listaData) {
                if (listaData[i].id == _id) return listaData[i];
            }
        }

        function LlenarTabla() {
            var tablaPedidos = "";
            var total = 0;
            var listaPedidos = JSON.parse(localStorage.getItem("pedidos"));
            if (listaPedidos.length > 0) {
                for (var i in listaPedidos) {
                    var data = BuscarPedidoEnListaGeneral(listaPedidos[i].idMenu);
                    var subtotal = parseFloat(Math.round(listaPedidos[i].subtotal * 100) / 100).toFixed(2);
                    tablaPedidos += '<tr>';
                    tablaPedidos += ' <td data-th="Pedido">'
                    tablaPedidos += '  <div class="row">'
                    tablaPedidos += '   <div class="col-sm-2 hidden-xs"><img src="../../Uploads/Menu/' + data.foto + '" alt="..." class="img-responsive" /></div>'
                    tablaPedidos += '   <div class="col-sm-10">'
                    tablaPedidos += '    <h4 class="nomargin text-uppercase text-center">' + data.titulo + '</h4>'
                    tablaPedidos += '    <p class="text-left">' + data.descripcion.substring(0, 100) + '...</p>'
                    tablaPedidos += '   </div>'
                    tablaPedidos += '  </div>'
                    tablaPedidos += ' </td>'
                    tablaPedidos += ' <td data-th="Precio">S/ ' + data.precioString + '</td>';
                    tablaPedidos += ' <td data-th="Cantidad">' + listaPedidos[i].cantidad + '</td>';
                    tablaPedidos += ' <td data-th="Subtotal" class="text-center">S/ ' + subtotal + '</td>';
                    tablaPedidos += ' <td class="actions" data-th="">';
                    tablaPedidos += '  <button class="btn btn-danger btn-sm"><i class="fa fa-trash-o"></i></button>';
                    tablaPedidos += ' </td>';
                    tablaPedidos += '</tr>';
                    total = total + parseFloat(listaPedidos[i].subtotal);
                }
            }
            
            $('.totalPedido').html('S/ ' + parseFloat(Math.round(total * 100) / 100).toFixed(2));
            $('.tb-resultadosPedido').html(tablaPedidos);
        }

        function Agregar(e) {
            e.preventDefault();

            var divPadre = $(this).parents("[data-pedido='producto']").eq(0);
            var _idMenu = parseInt($(divPadre).find('.idMenu').val());
            var _cantidad = parseInt($(divPadre).find('.cantidad').val());
            var existe = false;

            var listaPedidos = JSON.parse(localStorage.getItem("pedidos"));
            listaPedidos = listaPedidos == null ? [] : listaPedidos;

            if (listaPedidos.length > 0) {
                for (var i in listaPedidos) {
                    if (listaPedidos[i].idMenu == _idMenu) {
                        listaPedidos[i].cantidad = parseInt(_cantidad) + parseInt(listaPedidos[i].cantidad);
                        listaPedidos[i].subtotal = (listaPedidos[i].cantidad * listaPedidos[i].precio);
                        existe = true;
                        break;
                    }
                }
            }

            if (!existe) {
                for (var i in listaData) {
                    if (listaData[i].id == _idMenu) {
                        var data = {
                            idMenu: _idMenu,
                            cantidad: _cantidad,
                            precio: parseFloat(listaData[i].precioString),
                            subtotal: parseInt(_cantidad) * parseFloat(listaData[i].precioString)
                        }
                        listaPedidos.push(data);
                        break;
                    }
                }
            }

            localStorage.setItem("pedidos", JSON.stringify(listaPedidos));
            $(divPadre).find('.cantidad').val(1);

            LlenarTabla();
        }

        function DisminuirCantidad(e) {
            e.preventDefault();

            var divPadre = $(this).parents("[data-calculo='Pedido']").eq(0);
            var cantidad = $(divPadre).find('.cantidad').val();

            cantidad = parseInt(cantidad) - 1;

            if (parseInt(cantidad) == 0) {
                cantidad = 1;
            }

            $(divPadre).find('.cantidad').val(cantidad);
        }

        function AumentarCantidad(e) {
            e.preventDefault();

            var divPadre = $(this).parents("[data-calculo='Pedido']").eq(0);
            var cantidad = $(divPadre).find('.cantidad').val();
            $(divPadre).find('.cantidad').val(parseInt(cantidad) + 1);
        }

        return {
            Agregar: Agregar,
            DisminuirCantidad: DisminuirCantidad,
            AumentarCantidad: AumentarCantidad,
            LlenarTabla: LlenarTabla
        }
    })();

    me.Funciones = (function () {
        function inicializarEventos() {
            var body = $('body');
            body.on('click', '.disminuirCantidad', me.Eventos.DisminuirCantidad);
            body.on('click', '.aumentarCantidad', me.Eventos.AumentarCantidad);
            body.on('click', '.agregarPedido', me.Eventos.Agregar);

            me.Eventos.LlenarTabla();
        }
        return {
            inicializarEventos: inicializarEventos
        }
    })();

    me.Inicializar = function () {
        me.Funciones.inicializarEventos();
    }

    return me;

})(homeData, jQuery);

window.homeModule = homeModule;

$(document).ready(function () {
    homeModule.Inicializar();
});