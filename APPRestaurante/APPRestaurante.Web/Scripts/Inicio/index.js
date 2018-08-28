var homeModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;
    var listaData = globalData.listaDatos;

    me.Servicios = (function () {
        
    })();

    me.Eventos = (function () {
        function Agregar() {
            console.log(listaData);
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
            //me.Eventos.LlenaTabla();
        }
        return {
            inicializarEventos: inicializarEventos
        }
    })();

    me.Inicializar = function () {
        me.Funciones.inicializarEventos();
    }

    me.Agregar = function () {
        me.Eventos.Agregar();
    }

    return me;

})(homeData, jQuery);

window.homeModule = homeModule;

$(document).ready(function () {
    homeModule.Inicializar();
});