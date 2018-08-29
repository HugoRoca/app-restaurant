var homeModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;
    var listaData = globalData.listaDatos;

    me.Servicios = (function () {

    })();

    me.Eventos = (function () {
        function Agregar(_id) {
            var listaPedidos = JSON.parse(localStorage.getItem("pedidos"));
            listaPedidos = listaPedidos == null ? [] : listaPedidos;
            for (var item in listaData) {
                if (listaData[item].id == _id) {
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

        return {
            Agregar: Agregar
        }
    })();

    me.Funciones = (function () {
        function inicializarEventos() {
            var body = $('body');


            console.log(listaData);
        }
        return {
            inicializarEventos: inicializarEventos
        }
    })();

    me.Inicializar = function () {
        me.Funciones.inicializarEventos();
    }

    me.Agregar = function (_json) {
        console.log(_json);
        me.Eventos.Agregar(_json);
    }

    return me;

})(homeData, jQuery);

window.homeModule = homeModule;

$(document).ready(function () {
    homeModule.Inicializar();

    $('.count').prop('disabled', true);
    $(document).on('click', '.plus', function () {
        $('.count').val(parseInt($('.count').val()) + 1);
    });
    $(document).on('click', '.minus', function () {
        $('.count').val(parseInt($('.count').val()) - 1);
        if ($('.count').val() == 0) {
            $('.count').val(1);
        }
    });
});