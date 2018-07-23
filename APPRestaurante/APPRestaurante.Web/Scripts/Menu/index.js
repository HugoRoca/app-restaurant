var menuModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    me.Elementos = (function () {
        function getFechaDesde() { return $('.Desde'); }
        function getFechaHasta() { return $('.Hasta'); }
        return {
            getFechaDesde: getFechaDesde,
            getFechaHasta: getFechaHasta
        }
    })();

    me.Servicios = (function () {
        function listaMenu(_desde, _hasta) {
            return $.ajax({
                url: urls.urlBuscar,
                method: 'POST',
                data: {
                    desde: _desde,
                    hasta: _hasta,

                }
            });
        }
        return {}
    })();

    me.Eventos = (function () {
        function LlamarNuevoRegistro() {
            window.location.href = urls.llamaNuevoRegistro;
        }
        return {
            LlamarNuevoRegistro: LlamarNuevoRegistro
        }
    })();

    me.Funciones = (function () {
        function inicializarEventos() {
            var body = $('body');
            body.on('click', 'button[name=NuevoRegistro]', me.Eventos.LlamarNuevoRegistro);
            FuncionesGenerales.LlamarCalendario(me.Elementos.getFechaDesde());
            FuncionesGenerales.LlamarCalendario(me.Elementos.getFechaHasta());
        }
        return {
            inicializarEventos: inicializarEventos
        }
    })();

    me.Inicializar = function () {
        me.Funciones.inicializarEventos();
    }

    return me;

})(menuData, jQuery);

window.menuModule = menuModule;

$(document).ready(function () {
    menuModule.Inicializar();
});