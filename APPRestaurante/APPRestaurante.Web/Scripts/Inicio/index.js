var inicioModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    me.Services = (function () {
        function cerrarSesion() {
            return $.ajax({
                url: urls.cerrarSesion,
                method: 'POST'
            });
        }
        return {
            cerrarSesion: cerrarSesion
        }
    })();

    me.Eventos = (function () {
        function salir() {
            var successOut = function (r) {
                if (!r.Success) {
                    return;
                }
                window.location.href = urls.redirectLogin;
            }

            me.Services.cerrarSesion().then(successOut, function (e) {
                alert('Ocurrió un error inesperado');
            });
        }
        return {
            salir: salir
        }
    })();

    me.Funciones = (function () {
        function inicializarEventos() {
            var body = $('body');
            body.on('click', '#btnCerrarSession', me.Eventos.salir);
        }
        return {
            inicializarEventos: inicializarEventos
        }
    })();

    me.Inicializar = function () {
        me.Funciones.inicializarEventos();
    }

    return me;
})(inicioData, jQuery);

window.inicioModule = inicioModule;

$(document).ready(function () {
    inicioModule.Inicializar();
});