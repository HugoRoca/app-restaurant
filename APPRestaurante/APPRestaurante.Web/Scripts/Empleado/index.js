var empleadoModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    me.Servicios = (function () {
        function Lista() {
            return $.ajax({
                url: urls.urlListar,
                method: 'POST',
                data: {}
            });
        }
        return {
            Lista: Lista
        }
    })();

    me.Eventos = (function () {
        function LlenaTabla() {
            FuncionesGenerales.AbrirCargando();

            var successLista = function (r) {
                var tabla = '';
                for (var i = 0; i < r.length; i++) {
                    tabla += '<tr>';
                    tabla += '<td>' +
                        '<a class="btn btn-success btn-xs" href=""><i class="fa fa-edit"></i></a>' +
                        '<a class="btn btn-danger btn-xs" href="javascript:;"><i class="fa fa-trash"></i></a>' +
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
            });
        }
        return {
            LlenaTabla: LlenaTabla
        }
    })();

    me.Funciones = (function () {
        function inicializarEventos() {
            var body = $('body');
            me.Eventos.LlenaTabla();
        }
        return {
            inicializarEventos: inicializarEventos
        }
    })();

    me.Inicializar = function () {
        me.Funciones.inicializarEventos();
    }

    return me;

})(empleadoData, jQuery);

window.empleadoModule = empleadoModule;

$(document).ready(function () {
    empleadoModule.Inicializar();
});