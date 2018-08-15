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
                console.log(r);
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