var cobroModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    me.Elemtos = (function () {
        function getId() { return $('input[name=hdIdCobranza]'); }
        function getFecha() { return $('input[name=hdFecha]'); }
        function getTotal() { return $('input[name=hdTotal]'); }
        function getMesa() { return $('input[name=hdMesa]'); }
        function getNombres() { return $('input[name=Nombres]'); }
        function getTipoPago() { return $('select[name=TipoPago]'); }

        return {
            getId: getId,
            getFecha: getFecha,
            getTotal: getTotal,
            getMesa: getMesa,
            getNombres: getNombres,
            getTipoPago: getTipoPago
        }
    })();

    me.Servicios = (function () {
        function insertar(_data) {
            return $.ajax({
                url: urls.urlRegistro,
                method: 'POST',
                data: {
                    pedido: _data
                }
            });
        }

        return {
            insertar: insertar
        }
    })();

    me.Eventos = (function () {
        function insertar() {
            var model = {
                id: me.Elemtos.getId().val(),
                fecha: me.Elemtos.getFecha().val(),
                total: me.Elemtos.getTotal().val(),
                nombres: me.Elemtos.getNombres().val(),
                mesa: me.Elemtos.getMesa().val(),
                tipoPago: me.Elemtos.getTipoPago().val(),
                idEmpleado: 0,
                idUsuario: 0,
                estado: 2
            }

            var successInsertar = function (r) {
                window.location.href = urls.urlPaginaLista;
            }

            me.Servicios.insertar(model).then(successInsertar, function (e) {
                console.log(e);
            });
        }

        return {
            insertar: insertar
        }
    })();

    me.Funciones = (function () {
        function inicializarEventos() {
            var body = $('body');
            body.on('click', 'button[name=btnGuardar]', me.Eventos.insertar);
        }

        return {
            inicializarEventos: inicializarEventos
        }
    })();

    me.Inicializar = function () {
        me.Funciones.inicializarEventos();
    }

    return me;

})(cobroData, jQuery);

window.cobroModule = cobroModule;

$(document).ready(function () {
    cobroModule.Inicializar();
});