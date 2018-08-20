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

        function Eliminar(_id) {
            return $.ajax({
                url: urls.urlEliminar,
                method: 'POST',
                data: {
                    id: _id
                }
            });
        }

        return {
            Lista: Lista,
            Eliminar: Eliminar
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
            });
        }

        function EliminaRegistro(_id) {
            var successEliminar = function (r) {
                FuncionesGenerales.CerrarCargando();
                LlenaTabla();
            }

            bootbox.confirm("¿Está seguro de eliminar el registro?", function (result) {
                if (result) {
                    FuncionesGenerales.AbrirCargando();
                    me.Servicios.Eliminar(_id).then(successEliminar, function (e) {
                        FuncionesGenerales.CerrarCargando();
                        console.log(e);
                    });
                }
            });
        }

        return {
            LlenaTabla: LlenaTabla,
            EliminaRegistro: EliminaRegistro
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

    me.Eliminar = function (_id) {
        me.Eventos.EliminaRegistro(_id);
    }

    return me;

})(empleadoData, jQuery);

window.empleadoModule = empleadoModule;

$(document).ready(function () {
    empleadoModule.Inicializar();
});