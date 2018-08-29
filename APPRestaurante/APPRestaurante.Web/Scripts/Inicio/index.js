var homeModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;
    var listaData = globalData.listaDatos;

    me.Servicios = (function () {

    })();

    me.Eventos = (function () {
        function Agregar(e) {
            e.preventDefault();

            var divPadre = $(this).parents("[data-pedido='producto']").eq(0);
            console.log(divPadre);
            var _idMenu = parseInt($(divPadre).find('.idMenu').val());
            console.log(_idMenu);


            var listaPedidos = JSON.parse(localStorage.getItem("pedidos"));
            listaPedidos = listaPedidos == null ? [] : listaPedidos;
            for (var item in listaData) {
                if (listaData[item].id == _idMenu) {
                    listaPedidos.push(listaData[item]);
                    break;
                }
            }

            localStorage.setItem("pedidos", JSON.stringify(listaPedidos));
            /*FuncionesGenerales.AbrirCargando();

            var successLista = function (r) {
                var tabla = '';
                for (var i = 0; i < r.length; i++) {
                    tabla += '<tr>';
                    tabla += '<td>' +
                        '<a class="btn btn-success btn-xs" href="' + urls.llamaNuevoRegistro + '/' + r[i].id + '"> <i class="fa fa-edit"></i></a>' +
                        '<a class="btn btn-danger btn-xs" href="javascript:;" onclick="empleadoModule.Eliminar(' + r[i].id + ');"><i class="fa fa-trash"></i></a>' +
                        '</td>';
                    tabla += '<td>' + r[i].nombres + '</td>';
                    tabla += '<td>' + r[i].apellidos + '</td>';
                    tabla += '<td>' + r[i].direccion + '</td>';
                    tabla += '<td class="text-center">' + r[i].celular + '</td>';
                    tabla += '<td>' + r[i].tipoDocumento + '</td>';
                    tabla += '<td>' + r[i].documento + '</td>';
                    tabla += '<td class="text-center"><img src="../../Uploads/Empleado/' + r[i].foto + '" width="100"></td>';
                    tabla += '</tr>';
                }

                if (tabla != '') {
                    $('#ListaEmpleadoTbody').html(tabla);
                }

                FuncionesGenerales.CerrarCargando();
            };

            me.Servicios.Lista().then(successLista, function (e) {
                console.log(e);
                FuncionesGenerales.CerrarCargando();
            });*/
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
            AumentarCantidad: AumentarCantidad
        }
    })();

    me.Funciones = (function () {
        function inicializarEventos() {
            var body = $('body');
            body.on('click', '.disminuirCantidad', me.Eventos.DisminuirCantidad);
            body.on('click', '.aumentarCantidad', me.Eventos.AumentarCantidad);
            body.on('click', '.agregarPedido', me.Eventos.Agregar);

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