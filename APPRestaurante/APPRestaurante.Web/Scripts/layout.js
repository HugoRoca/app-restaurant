var layoutModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    me.Elements = (function () {
        function setSpanNombreUsuario() { return $('#spanNombre'); }
        return {
            setSpanNombreUsuario: setSpanNombreUsuario
        }
    })();

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
})(layoutData, jQuery);

window.layoutModule = layoutModule;

$(document).ready(function () {
    layoutModule.Inicializar();
});