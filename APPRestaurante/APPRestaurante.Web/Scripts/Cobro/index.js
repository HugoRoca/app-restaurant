var cobroModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    me.Servicios = (function () {

    })();

    me.Eventos = (function () {

    })();

    me.Funciones = (function () {

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